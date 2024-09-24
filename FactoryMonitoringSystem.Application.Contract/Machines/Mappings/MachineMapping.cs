using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Request;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Responses;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Mapster;


namespace FactoryMonitoringSystem.Application.Contracts.Machines.Mappings
{
    public class MachineMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MachineRequest, Machine > ()
            .Map(dest => dest.Sensors, src => src.Sensors.Adapt<List<Sensor>>());
            config.NewConfig<Machine, MachineResponse>()
            .Map(dest => dest.Sensors, src => src.Sensors.Adapt<List<SensorResponse>>());

        }
    }
}
