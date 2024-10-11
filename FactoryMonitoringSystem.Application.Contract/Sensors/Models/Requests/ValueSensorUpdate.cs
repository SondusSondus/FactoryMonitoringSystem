namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Requests
{
    public record ValueSensorUpdate
    {
        public Guid SensorId { get; set; }
        public double NewValue { get; set; }
    }
}
