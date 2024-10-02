

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request
{
    public record SensorRequest
    {
        public Guid MachineId { get; set; }
        public string SensorType { get; set; }
        public string Unit { get; set; }
    }
}
