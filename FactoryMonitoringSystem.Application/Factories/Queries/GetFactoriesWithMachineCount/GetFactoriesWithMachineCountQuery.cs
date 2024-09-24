using FactoryMonitoringSystem.Domain.Shared;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesWithMachineCount
{
    public record GetFactoriesWithMachineCountQuery : IRequest<List<FactoryWithMachineCountResponse>>;

}
