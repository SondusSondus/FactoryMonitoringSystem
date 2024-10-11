using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.CreateMachine
{
    public record CreateMachineCommand(MachineRequest MachineRequest) : IRequest<ErrorOr<Success>>;

}
