using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using Mapster;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Mappings
{
    public class FactoryMapping : IProfile
    {

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FactoryRequest, Factory>();


            // Configure Mapster to use the constructor
            TypeAdapterConfig<UpdateFactoryRequest, Factory>
                .NewConfig()
                .MapToConstructor(true);

            config.NewConfig<Factory, FactoryResponse>()
                             .Map(dest => dest.Machines, src => src.Machines.Adapt<List<MachineResponse>>());

        }
    }
}
