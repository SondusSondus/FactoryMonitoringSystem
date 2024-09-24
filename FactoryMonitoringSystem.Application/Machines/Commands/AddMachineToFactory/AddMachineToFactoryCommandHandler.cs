using MediatR;


namespace FactoryMonitoringSystem.Application.Machines.Commands.AddMachineToFactory
{
    public class AddMachineToFactoryCommandHandler : IRequestHandler<AddMachineToFactoryCommand, bool>
    {
        public Task<bool> Handle(AddMachineToFactoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
