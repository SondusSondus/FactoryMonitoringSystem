using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public class DeleteSensorCommandHandler(ISensorService sensorService) : IRequestHandler<DeleteSensorCommand, ErrorOr<Success>>
    {
        private readonly ISensorService _sensorService = sensorService;
        async Task<ErrorOr<Success>> IRequestHandler<DeleteSensorCommand, ErrorOr<Success>>.Handle(DeleteSensorCommand request, CancellationToken cancellationToken)
                   => await _sensorService.DeleteSensorAsync(request.SensorId, cancellationToken);
    }
}
