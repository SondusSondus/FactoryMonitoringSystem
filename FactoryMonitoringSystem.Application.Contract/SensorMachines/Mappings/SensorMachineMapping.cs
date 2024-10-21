using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Mappings
{
    public class SensorMachineMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<SensorMachine, SensorMachineResponse>();
            //config.NewConfig<SensorMachineResquest, SensorMachine>();
            
        }
    }
}
