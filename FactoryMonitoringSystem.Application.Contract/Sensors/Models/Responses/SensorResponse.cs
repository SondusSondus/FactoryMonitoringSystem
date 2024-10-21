

using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Domain.Shared.Sensor.Enum;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses
{
    public record SensorResponse
    {
        public Guid Id { get; set; }
        public SensorEnumType Type { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    } 
    public record SensorResponseForSensorMachine
    {
        public Guid Id { get; set; }
        public SensorEnumType Type { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
    }
}
