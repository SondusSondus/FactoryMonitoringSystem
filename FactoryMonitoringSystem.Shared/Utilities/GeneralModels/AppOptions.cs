

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public record AppOptions
    {
        public const string Section = "AppOptions";
        public string WriteDatabaseConnectionString { get; set; }

        public string ReadDatabaseConnectionString { get; set; }
        public int LockoutDurationMinutes { get; set; }
        public int MaxFailedAttempts { get; set; }

    }

}
