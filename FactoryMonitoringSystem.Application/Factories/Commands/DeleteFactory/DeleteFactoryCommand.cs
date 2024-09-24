using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory
{
    public record DeleteFactoryCommand(Guid factoryId) :IRequest<bool>;
   
}
