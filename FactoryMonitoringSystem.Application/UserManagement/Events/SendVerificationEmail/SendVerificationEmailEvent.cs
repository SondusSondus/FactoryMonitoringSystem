using FactoryMonitoringSystem.Application.Common.Events;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;


namespace FactoryMonitoringSystem.Application.UserManagement.Events.SendVerificationEmail
{
    public record SendVerificationEmailEvent(EmailModel EmailModel) : NotificationEvent<EmailModel>
    {
        public List<NotificationSystemModelEnum> GetNotificationSystems()
        {
            return base.NotificationSystems = new List<NotificationSystemModelEnum>()
            {
                NotificationSystemModelEnum.EmailNotification,
            };
        }
    }
}
