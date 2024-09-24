using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.Machines.Commands.AddMachineToFactory
{
    public record AddMachineToFactoryCommand(MachineRequest MachineToFactory) : IRequest<bool>, IApplicationAssemblyMarker;
}
