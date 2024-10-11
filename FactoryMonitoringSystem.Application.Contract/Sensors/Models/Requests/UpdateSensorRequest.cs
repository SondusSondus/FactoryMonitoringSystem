

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request
{
    public record UpdateSensorRequest : SensorRequest
    {
        public Guid Id { get; set; }
   
    }
}
