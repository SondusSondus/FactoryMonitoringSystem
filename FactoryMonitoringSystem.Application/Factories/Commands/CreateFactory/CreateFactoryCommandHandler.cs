using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory
{
    internal class CreateFactoryCommandHandler(IFactoryService factoryService) : IRequestHandler<CreateFactoryCommand, ErrorOr<Success>>
    {
        private readonly IFactoryService _factoryService= factoryService;
        public async Task<ErrorOr<Success>> Handle(CreateFactoryCommand request, CancellationToken cancellationToken)
        => await _factoryService.CreateFactoryAsync(request.FactoryRequet, cancellationToken);

    }
}
