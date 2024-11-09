using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.DeleteSensor
{
    public record DeleteSensorCommand(Guid Id) : IRequest<ErrorOr<Success>>;
}
