using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory
{
    public class DeleteFactoryCommandHandler : IRequestHandler<DeleteFactoryCommand, bool>
    {
        public Task<bool> Handle(DeleteFactoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
