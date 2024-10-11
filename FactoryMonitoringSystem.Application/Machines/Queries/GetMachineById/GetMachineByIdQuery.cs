using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Queries.GetMachineById
{
    public record GetMachineByIdQuery(Guid Id) : IRequest<ErrorOr<MachineResponse>>;

}
