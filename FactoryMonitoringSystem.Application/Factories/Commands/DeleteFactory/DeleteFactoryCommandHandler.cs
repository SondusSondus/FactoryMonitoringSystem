using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory
{
    internal class DeleteFactoryCommandHandler(IFactoryService factoryService) : IRequestHandler<DeleteFactoryCommand, ErrorOr<Success>>
    {
        private readonly IFactoryService _factoryService = factoryService;
        async Task<ErrorOr<Success>> IRequestHandler<DeleteFactoryCommand, ErrorOr<Success>>.Handle(DeleteFactoryCommand request, CancellationToken cancellationToken)
          => await _factoryService.DeleteFactoryAsync(request.factoryId, cancellationToken);
    }
}
