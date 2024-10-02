using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Mapster;


namespace FactoryMonitoringSystem.Application.Common.Services
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly IEmailService _emailService;

        public EmailNotificationStrategy(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendNotificationAsync<T>(T notification, NotificationSystemModelEnum notificationSystem) where T : class
        {
            if (notificationSystem is NotificationSystemModelEnum.EmailNotification)
            {
                var emailNotification = notification.Adapt<EmailModel>();
                await _emailService.SendEmailAsync(emailNotification);

            }
        }
    }
}
