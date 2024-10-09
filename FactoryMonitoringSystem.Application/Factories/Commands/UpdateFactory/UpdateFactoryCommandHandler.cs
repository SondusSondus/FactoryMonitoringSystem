using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactory
{
    internal class UpdateFactoryCommandHandler(IFactoryService factoryService) : IRequestHandler<UpdateFactoryCommand, ErrorOr<Success>>
    {
        private readonly IFactoryService _factoryService = factoryService;
        public async Task<ErrorOr<Success>> Handle(UpdateFactoryCommand request, CancellationToken cancellationToken)
            => await _factoryService.UpdateFactoryAsync(request.FactoryRequet,cancellationToken);
      
    }
}
