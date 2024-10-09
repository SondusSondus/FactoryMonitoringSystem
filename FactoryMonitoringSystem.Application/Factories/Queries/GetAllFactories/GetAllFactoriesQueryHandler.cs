using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetAllFactories
{
    public class GetAllFactoriesQueryHandler(IFactoryService factoryService) : IRequestHandler<GetAllFactoriesQuery, ErrorOr<List<FactoryResponse>>>
    {
        private readonly IFactoryService _factoryService = factoryService;

        public async Task<ErrorOr<List<FactoryResponse>>> Handle(GetAllFactoriesQuery request, CancellationToken cancellationToken)
                => await _factoryService.GetAllFactoriesAsync(cancellationToken);
    }
}
