using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddSensorToMachine
{
    public record AddSensorToMachineCommand(SensorMachineResquest SensorMachine) : IRequest<ErrorOr<Success>>;
}
