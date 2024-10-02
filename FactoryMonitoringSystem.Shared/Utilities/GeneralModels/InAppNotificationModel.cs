

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public record InAppNotificationModel
    {
        public string UserEmail { get; set; }
        public string Message { get; set; }

        public InAppNotificationModel(string userEmail, string message)
        {
            Message= message;
            UserEmail = userEmail;

        }
    }
}
