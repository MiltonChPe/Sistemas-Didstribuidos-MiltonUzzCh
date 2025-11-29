using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    
    private readonly ILogger<EmailService> _logger;
    private readonly IConfiguration _configuration;
    private readonly SendGridClient _client;

    private readonly string _senderEmail;
    private readonly string _senderName;


    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;

        _senderEmail = configuration["SendGrid:SenderEmail"];
        _senderName = configuration["SendGrid:SenderName"];
        _client = new SendGridClient(configuration["SendGrid:ApiKey"]);
    }


    public async Task SendWelcomeEmailAsync(string toEmail, string trainerName)
    {
        var subject = "Welcome to the team!";
        var htmlContent = $"<strong>Dear {trainerName}, welcome to our training platform!</strong>";
        await SendEmail(toEmail, trainerName, subject, htmlContent);
    }

    private async Task SendEmail(string toEmail,string toName, string subject, string htmlContent)
    {
        var from = new EmailAddress(_senderEmail, _senderName);
        var to = new EmailAddress(toEmail, toName);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: "", htmlContent: htmlContent);

        try
        {
            var response = await _client.SendEmailAsync(msg);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Email sent to {Email}", toEmail);
            }
            else
            {
                _logger.LogError("Failed to send email to {Email}. Status Code: {StatusCode}", toEmail, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}", toEmail);
        }
    }
}