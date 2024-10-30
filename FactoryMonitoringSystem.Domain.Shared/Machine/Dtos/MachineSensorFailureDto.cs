namespace FactoryMonitoringSystem.Domain.Shared.Machine.Dtos
{
    public record MachineSensorFailureDto
    {

        public string MachineName { get; set; }
        public string SensorName { get; set; }
        public double Value { get; set; }
        public string ErrorMessage { get; set; }
        public string Severity { get; set; } // Example: "Critical", "Warning"
        public DateTime Timestamp { get; set; }
    }
}
