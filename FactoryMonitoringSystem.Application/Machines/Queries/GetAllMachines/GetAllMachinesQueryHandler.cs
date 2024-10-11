using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Machines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Queries.GetAllMachines
{
    internal class GetAllMachinesQueryHandler(IMachineService machineService) : IRequestHandler<GetAllMachinesQuery, ErrorOr<List<MachineResponse>>>
    {
        private readonly IMachineService _machineService = machineService;
        public async Task<ErrorOr<List<MachineResponse>>> Handle(GetAllMachinesQuery request, CancellationToken cancellationToken)
           => await _machineService.GetAllMachinesAsync(cancellationToken);
    }
}
