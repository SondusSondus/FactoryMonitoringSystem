

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{
    public record HangfireStorageSettings
    {
        public const string SectionServer = "HangfireSettings:Server";
        public const string SectionStorage = "HangfireSettings:Storage";
        public string? StorageProvider { get; set; }
        public string? ConnectionString { get; set; }
    }
}
