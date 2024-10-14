namespace FactoryMonitoringSystem.Domain.Shared.Sensor.Dtos
{
    public record SensorThresholdAlertDto
    {
        public Guid SensorId { get; set; }
        public string SensorName { get; set; }
        public double CurrentValue { get; set; }
        public double Threshold { get; set; }
        public string Status { get; set; } // Example: "Above Threshold", "Below Threshold"
        public DateTime Timestamp { get; set; }
    }
}
