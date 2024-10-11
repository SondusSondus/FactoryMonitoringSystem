using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.CreateMachine
{
    internal class CreateMachineCommandHandler(IMachineService machineService) : IRequestHandler<CreateMachineCommand, ErrorOr<Success>>
    {
        private readonly IMachineService _machineService = machineService;
        public async Task<ErrorOr<Success>> Handle(CreateMachineCommand request, CancellationToken cancellationToken)
                => await _machineService.CreateMachineAsync(request.MachineRequest, cancellationToken);
    }
}
