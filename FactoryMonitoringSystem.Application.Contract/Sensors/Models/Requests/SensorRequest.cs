

using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request
{
    public record SensorRequest
    {
        public string Name { get; set; }
        public SensorEnumType Type { get; set; }
        public string Unit { get; set; }
        public Guid MachineId { get; set; }
    }
}
