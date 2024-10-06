using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;


namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public class EmailService : IEmailService, IScopedDependency
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        // Enqueue email sending as a background job
        public Task SendEmailAsync(EmailModel emailModel,CancellationToken cancellationToken)
        {
            BackgroundJob.Enqueue(() => SendEmail(emailModel, cancellationToken));
            return Task.CompletedTask;
        }

        // This is the actual method that will be run in the background by Hangfire
        public async Task SendEmail(EmailModel emailModel,CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Send email to user {emailModel.To}");

            using (var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port))
            {
                client.Credentials = new System.Net.NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                client.EnableSsl = _emailSettings.EnableSsl;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail),
                    Subject = emailModel.Subject,
                    Body = emailModel.Body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(emailModel.To);
                try
                {
                    await client.SendMailAsync(mailMessage,cancellationToken);
                    _logger.LogError($"Email sent to {emailModel.To}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to send email to {emailModel.To}: {ex.Message}");
                    throw;
                }
                _logger.LogInformation($"Send rmail to user successfully");

            }

        }

        
    }
}