using TrainerApi.Events;

namespace TrainerApi.Infrastructure.Producers;

public class KafkaProducer : IMessageBrokerProducer, IDisposable
{   
    public async Task ProduceAsync<T>(T message) where T : class, IEventMessage
    {
        
    }
    public void Dispose()
    {

    }
   
}