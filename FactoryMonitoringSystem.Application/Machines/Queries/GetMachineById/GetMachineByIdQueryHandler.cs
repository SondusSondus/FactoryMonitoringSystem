using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Queries.GetMachineById
{
    internal class GetMachineByIdQueryHandler(IMachineService machineService) : IRequestHandler<GetMachineByIdQuery, ErrorOr<MachineResponse>>
    {
        private readonly IMachineService _machineService =machineService;
        public async Task<ErrorOr<MachineResponse>> Handle(GetMachineByIdQuery request, CancellationToken cancellationToken)
               => await _machineService.GetMachineByIdAsync(request.Id, cancellationToken);
    }
}
