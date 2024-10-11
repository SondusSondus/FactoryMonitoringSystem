using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using MediatR;


namespace FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine
{
    public record CreateSensorCommand(SensorRequest Sensor) : IRequest<ErrorOr<Success>>;
   
}
