using FactoryMonitoringSystem.Application.Common.Events;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;


namespace FactoryMonitoringSystem.Application.UserManagement.Events.SendVerificationEmail
{
    public record SendVerificationEmailEvent(EmailModel EmailModel) : NotificationEvent 
    {
        public override List<NotificationSystemModelEnum> NotificationSystems { get; set; } = new()
    {
        NotificationSystemModelEnum.EmailNotification
    };

        public override object NotificationObject { get; set; } = EmailModel;
    }
}

