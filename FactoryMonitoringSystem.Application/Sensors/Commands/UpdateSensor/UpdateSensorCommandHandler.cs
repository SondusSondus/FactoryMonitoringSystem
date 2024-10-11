using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public class UpdateSensorCommandHandler(ISensorService sensorService) : IRequestHandler<UpdateSensorCommand, ErrorOr<Success>>
    {
        private readonly ISensorService _sensorService = sensorService;
        async Task<ErrorOr<Success>> IRequestHandler<UpdateSensorCommand, ErrorOr<Success>>.Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
          => await _sensorService.UpdateSensorAsync(request.UpdateSensor, cancellationToken);
    }
}
