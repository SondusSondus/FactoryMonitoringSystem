using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public record DeleteSensorCommand(Guid SensorId) : IRequest<ErrorOr<Success>>;
}
