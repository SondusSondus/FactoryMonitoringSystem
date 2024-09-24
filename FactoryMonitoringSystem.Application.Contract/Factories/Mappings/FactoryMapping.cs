using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Contracts.Factories.Mappings
{
    public class FactoryMapping : IProfile
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FactoryRequet, Factory>()
                            .Map(dest => dest.Machines, src => src.Machines.Adapt<List<Machine>>());

            config.NewConfig<Factory, FactoryResponse>()
                             .Map(dest => dest.Machines, src => src.Machines.Adapt<List<MachineResponse>>());

        }
    }
}
