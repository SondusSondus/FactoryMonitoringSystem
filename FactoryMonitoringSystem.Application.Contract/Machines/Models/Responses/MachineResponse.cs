using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Domain.Shared.Machine.Enum;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses
{
    public record MachineResponse
    {
        public Guid Id { get; set; }
        public Guid FactoryId { get; set; }
        public string Name { get; set; }
        public MachineTypeEnum Type { get; set; }
        public string SerialNumber { get; set; }
        public List<SensorMachineWithSensorResponse> SensorMachines { get; set; }
    }
}
