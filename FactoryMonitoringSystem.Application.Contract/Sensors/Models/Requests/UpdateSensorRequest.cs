

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request
{
    public record UpdateSensorRequest
    {
        public Guid SensorId { get; set; }
        public double NewValue { get; set; }
    }
}
