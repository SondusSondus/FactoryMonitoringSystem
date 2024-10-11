using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine
{
    internal class CreateSensorCommandHandler(ISensorService sensorService) : IRequestHandler<CreateSensorCommand, ErrorOr<Success>>
    {
        private readonly ISensorService _sensorService = sensorService;
        public async Task<ErrorOr<Success>> Handle(CreateSensorCommand request, CancellationToken cancellationToken)
            => await _sensorService.CreateSensorAsync(request.Sensor, cancellationToken);
    }
}
