using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses
{
    public record SensorMachineResponse
    {
        public Guid SensorId { get; set; }
        public Guid MachineId { get; set; }

        virtual public MachineResponse machine { get; set; }
        virtual public SensorResponse Sensor { get; set; }
    } 
    public record SensorMachineWithSensorResponse
    {
        public Guid SensorId { get; set; }

        virtual public SensorResponse Sensor { get; set; }
    } 
    
    public record SensorMachineWithMachineResponse
    {
        public Guid SensorId { get; set; }

        virtual public SensorResponse Sensor { get; set; }
    }
}
