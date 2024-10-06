using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Queries.GetFactoryById
{
    public class GetFactoryByIdQueryHandler : IRequestHandler<GetFactoryByIdQuery, ErrorOr<FactoryResponse>>
    {
        private readonly IFactoryService _factoryService;
        public GetFactoryByIdQueryHandler(IFactoryService factoryService)
        {
            _factoryService = factoryService;
        }

        public async Task<ErrorOr<FactoryResponse>> Handle(GetFactoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _factoryService.GetFactoryByIdAsync(request.FactoryId, cancellationToken);
        }

        
    }
}
