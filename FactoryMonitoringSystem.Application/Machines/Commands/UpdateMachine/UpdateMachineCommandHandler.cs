using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine
{
    internal class UpdateMachineCommandHandler(IMachineService machineService) : IRequestHandler<UpdateMachineCommand, ErrorOr<Success>>
    {
        private readonly IMachineService _machineService = machineService;
        public async Task<ErrorOr<Success>> Handle(UpdateMachineCommand request, CancellationToken cancellationToken)
           => await _machineService.UpdateMachineAsync(request.UpdateMachine, cancellationToken);
    }
}
