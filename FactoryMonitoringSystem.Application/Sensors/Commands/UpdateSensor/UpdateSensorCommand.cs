using FactoryMonitoringSystem.Application.Contracts.Sensors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public record UpdateSensorCommand(UpdateSensorRequest UpdateSensor):IRequest<bool>;
  
}
