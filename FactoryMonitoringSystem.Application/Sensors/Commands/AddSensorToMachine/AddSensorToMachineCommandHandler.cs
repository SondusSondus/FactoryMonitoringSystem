using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine
{
    public class AddSensorToMachineCommandHandler : IRequestHandler<AddSensorToMachineCommand, bool>
    {
        public Task<bool> Handle(AddSensorToMachineCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
