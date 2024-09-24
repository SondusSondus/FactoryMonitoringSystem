using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public record DeleteSensorCommand(Guid SensorId) : IRequest<bool>;
}
