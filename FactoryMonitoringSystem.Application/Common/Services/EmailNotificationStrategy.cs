using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;


namespace FactoryMonitoringSystem.Application.Common.Services
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly IEmailService _emailService;

        public EmailNotificationStrategy(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendNotificationAsync<T>(T notification) where T : class
        {

            if (notification.GetType().GetProperty("EmailModel").GetValue(notification) is EmailModel emailNotification)
            {
                await _emailService.SendEmailAsync(emailNotification);
            }
        }
    }
}
