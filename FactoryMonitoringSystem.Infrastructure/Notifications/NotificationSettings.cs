

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public record NotificationSettings
    {
        public const string Section = "NotificationSettings";
        public bool EnableInAppNotifications { get; set; }
    }
}
