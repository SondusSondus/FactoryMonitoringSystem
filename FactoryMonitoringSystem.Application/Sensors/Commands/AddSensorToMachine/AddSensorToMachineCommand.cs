using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using MediatR;


namespace FactoryMonitoringSystem.Application.Sensors.Commands.AddSensorToMachine
{
    public record AddSensorToMachineCommand(SensorRequest Sensor) : IRequest<bool>;
   
}
