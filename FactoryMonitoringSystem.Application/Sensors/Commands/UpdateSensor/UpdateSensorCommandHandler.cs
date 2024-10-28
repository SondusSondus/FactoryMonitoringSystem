using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    internal class UpdateSensorCommandHandler(ISensorService sensorService) : IRequestHandler<UpdateSensorCommand, ErrorOr<Success>>
    {
        private readonly ISensorService _sensorService = sensorService;
        async Task<ErrorOr<Success>> IRequestHandler<UpdateSensorCommand, ErrorOr<Success>>.Handle(UpdateSensorCommand request, CancellationToken cancellationToken)
          => await _sensorService.UpdateSensorAsync(request.id, request.updateSensor, cancellationToken);
    }
}
