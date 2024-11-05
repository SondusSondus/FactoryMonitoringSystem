using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using FactoryMonitoringSystem.Domain.Shared.Factory.Models;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesWithMachineCount
{
    internal class GetFactoriesWithMachineCountQueryHandler : IRequestHandler<GetFactoriesWithMachineCountQuery, List<FactoryWithMachineCountResponse>>
    {
        private readonly IFactoryService _factoryService;

        public GetFactoriesWithMachineCountQueryHandler(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }
        public async Task<List<FactoryWithMachineCountResponse>> Handle(GetFactoriesWithMachineCountQuery request, CancellationToken cancellationToken)
        {
            return await _factoryService.GetFactoriesWithMachineCountAsync(cancellationToken);
        }
    }
}
