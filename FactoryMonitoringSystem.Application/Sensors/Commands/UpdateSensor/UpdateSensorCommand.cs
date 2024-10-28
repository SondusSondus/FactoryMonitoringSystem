using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Commands.UpdateSensor
{
    public record UpdateSensorCommand(Guid id ,SensorRequest updateSensor) : IRequest<ErrorOr<Success>>;

}
