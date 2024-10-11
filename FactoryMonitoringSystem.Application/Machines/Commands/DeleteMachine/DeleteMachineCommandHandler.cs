using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.DeleteMachine
{
    internal class DeleteMachineCommandHandler(IMachineService machineService) : IRequestHandler<DeleteMachineCommand, ErrorOr<Success>>
    {
        private readonly IMachineService _machineService = machineService;
        public async Task<ErrorOr<Success>> Handle(DeleteMachineCommand request, CancellationToken cancellationToken)
                => await _machineService.DeleteMachineAsync(request.Id, cancellationToken);
    }
}
