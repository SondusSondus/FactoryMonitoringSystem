

namespace FactoryMonitoringSystem.Shared.Utilities.GeneralModels
{
    public record HangfireStorageSettings
    {
        public string? StorageProvider { get; set; }
        public string? ConnectionString { get; set; }
    }
}
