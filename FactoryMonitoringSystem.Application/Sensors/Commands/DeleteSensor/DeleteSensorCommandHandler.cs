using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand, bool>
    {
        public Task<bool> Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
