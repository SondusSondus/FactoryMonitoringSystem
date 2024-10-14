

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public record MonitoringSettings
    {
        public const string Section = "MonitoringSettings";
        public bool EnableInAppNotifications { get; set; }
    }
}
