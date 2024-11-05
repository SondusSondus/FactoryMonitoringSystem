using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine
{
    internal class UpdateMachineCommandHandler(IMachineService machineService) : IRequestHandler<UpdateMachineCommand, ErrorOr<Success>>
    {
        private readonly IMachineService _machineService = machineService;
        public async Task<ErrorOr<Success>> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
           => await _machineService.UpdateMachineAsync(request.id, request.updateMachine, cancellationToken);
    }
}
