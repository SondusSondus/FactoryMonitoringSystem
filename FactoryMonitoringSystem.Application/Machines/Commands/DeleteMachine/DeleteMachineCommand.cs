using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.DeleteMachine
{
    public record DeleteMachineCommand(Guid Id) :IRequest<ErrorOr<Success>>;
    
}
