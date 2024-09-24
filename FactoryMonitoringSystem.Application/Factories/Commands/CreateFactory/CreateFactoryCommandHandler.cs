using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory
{
    public class CreateFactoryCommandHandler : IRequestHandler<CreateFactoryCommand, ErrorOr<Guid>>
    {
        private readonly IFactoryService _factoryService;

        public CreateFactoryCommandHandler(IFactoryService factoryService) =>
            _factoryService = factoryService;


        public async Task<ErrorOr<Guid>> Handle(CreateFactoryCommand request, CancellationToken cancellationToken)
        {
            return await _factoryService.CreateFactoryAsync(request.FactoryRequet);
        }
    }
}
