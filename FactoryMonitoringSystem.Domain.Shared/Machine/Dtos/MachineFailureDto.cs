namespace FactoryMonitoringSystem.Domain.Shared.Machine.Dtos
{
    public record MachineFailureDto
    {

        public string MachineId { get; set; }
        public string MachineName { get; set; }
        public string ErrorMessage { get; set; }
        public string Severity { get; set; } // Example: "Critical", "Warning"
        public DateTime Timestamp { get; set; }
    }
}
