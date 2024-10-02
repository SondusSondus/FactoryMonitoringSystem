using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Hangfire;
using Microsoft.Extensions.Options;
using System.Net.Mail;


namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public class EmailService : IEmailService, ITransientDependency
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        // Enqueue email sending as a background job
        public Task SendEmailAsync(EmailModel emailModel)
        {
            BackgroundJob.Enqueue(() => SendEmail(emailModel));
            return Task.CompletedTask;
        }

        // This is the actual method that will be run in the background by Hangfire
        public async Task SendEmail(EmailModel emailModel)
        {
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
                    await client.SendMailAsync(mailMessage);
                    Console.WriteLine($"Email sent to {emailModel.To}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email to {emailModel.To}: {ex.Message}");
                    throw;
                }
            }

        }

        
    }
}