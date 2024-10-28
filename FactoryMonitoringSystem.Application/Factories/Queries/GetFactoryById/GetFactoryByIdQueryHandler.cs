using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById
{
    public class GetFactoryByIdQueryHandler(IFactoryService factoryService) : IRequestHandler<GetFactoryByIdQuery, ErrorOr<FactoryResponse>>
    {
        private readonly IFactoryService _factoryService = factoryService;
        public async Task<ErrorOr<FactoryResponse>> Handle(GetFactoryByIdQuery request, CancellationToken cancellationToken)
        => await _factoryService.GetFactoryByIdAsync(request.id, cancellationToken);

    }
}
