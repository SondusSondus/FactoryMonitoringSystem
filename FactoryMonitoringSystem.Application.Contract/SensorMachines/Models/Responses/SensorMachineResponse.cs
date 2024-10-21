using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses
{
    public record SensorMachineResponse
    {
        public Guid Id { get; set; }
        public Guid SensorId { get; set; }
        public Guid MachineId { get; set; }

        virtual public MachineResponseForSensorMachine Machine { get; set; }
        virtual public SensorResponse Sensor { get; set; }
    } 
    public record SensorMachineWithSensorResponse
    {
        virtual public SensorResponseForSensorMachine Sensor { get; set; }
    } 
    
   
}
