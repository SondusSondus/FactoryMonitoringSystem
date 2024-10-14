using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using Mapster;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Mappings
{
    public class MachineMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MachineRequest, Machine>();
            config.NewConfig<UpdateMachineRequest, Machine>();
            config.NewConfig<Machine, MachineResponse>()
            .Map(dest => dest.SensorMachines, src => src.SensorMachines.Adapt<List<SensorMachineWithSensorResponse>>());

        }
    }
}
