using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Queries.GetAllMachines
{
    public record GetAllMachinesQuery : IRequest<ErrorOr<List<MachineResponse>>>;

}
