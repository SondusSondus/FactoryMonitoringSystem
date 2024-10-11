using FactoryMonitoringSystem.Domain.Shared.Factory.Models;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesWithMachineCount
{
    public record GetFactoriesWithMachineCountQuery : IRequest<List<FactoryWithMachineCountResponse>>;

}
