using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand, bool>
    {
        public Task<bool> Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
