using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.UpdateMachine
{
    public record UpdateMachineCommand(Guid id,MachineRequest updateMachine) : IRequest<ErrorOr<Success>>;

}
