using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Newtonsoft.Json;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).
    AddEnvironmentVariables();

var configuration = builder.Build();
var ServiceCollection = new ServiceCollection();
ServiceCollection.AddSingleton<IConfiguration>(configuration);
ServiceCollection.AddSingleton<EmailService>();
ServiceCollection.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.SetMinimumLevel(LogLevel.Information);
});



var serviceProvider = ServiceCollection.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
var emailService = serviceProvider.GetRequiredService<EmailService>();

logger.LogInformation("Starting Kafka Email Consumer");

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = configuration.GetValue<string>("Kafka:BootstrapServers"),
    GroupId = configuration.GetValue<string>("Kafka:GroupId"),
    AutoOffsetReset = AutoOffsetReset.Earliest,
};

LogLevel MapKafkaLogLevel(Confluent.Kafka.SyslogLevel kafkaLevel) 
    {
       return kafkaLevel switch
        {
            Confluent.Kafka.SyslogLevel.Error => LogLevel.Error,
            Confluent.Kafka.SyslogLevel.Warning => LogLevel.Warning,
            Confluent.Kafka.SyslogLevel.Info => LogLevel.Information,
            Confluent.Kafka.SyslogLevel.Debug => LogLevel.Debug,
            _ => LogLevel.Trace,
        };
    };

using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig)
.SetLogHandler((_, logMessage) =>
{
   logger.Log(MapKafkaLogLevel(logMessage.Level), $"Kafka Log: {logMessage.Message}");
}).Build();

var topics = configuration.GetSection("Kafka:Topics").Get<string[]>();
logger.LogInformation("Subscribing to topics: {Topics}", string.Join(", ", topics));

consumer.Subscribe(topics);

try
{
    while (true)
    {
        var consumeResult = consumer.Consume();

        logger.LogInformation("[{Topic}] Message: {Message}",
            consumeResult.Topic,
            consumeResult.Message.Value);

        try{
            dynamic data = JsonConvert.DeserializeObject(consumeResult.Message.Value);
            string name = data.name;
            switch (consumeResult.Topic){
                case "trainer-created":
                    await emailService.SendWelcomeEmailAsync("mchable10@alumnos.uaq.mx", name);
                    break;
                default:
                    logger.LogWarning("No handler for topic: {Topic}", consumeResult.Topic);
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error processing message: {Message}" + ex.Message);
        }
            
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
    logger.LogError("Kafka Log Consumer stopped");
}