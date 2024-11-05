using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoriesByLocation
{
    internal class GetFactoriesByLocationQueryHandler : IRequestHandler<GetFactoriesByLocationQuery, List<FactoryResponse>>
    {
        private readonly IFactoryService _factoryService;

        public GetFactoriesByLocationQueryHandler(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }
        public async Task<List<FactoryResponse>> Handle(GetFactoriesByLocationQuery request, CancellationToken cancellationToken)
        {
            return await _factoryService.GetFactoriesByLocationAsync(request.location, cancellationToken);
        }
    }
}
