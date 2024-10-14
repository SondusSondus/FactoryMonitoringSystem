using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Domain.SensorMachine.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Mappings
{
    public class SensorMachineMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Sensor, SensorMachineResponse>();
            config.NewConfig<SensorMachineResquest, SensorMachine>();
            
        }
    }
}
