using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddValueSensorForMachine
{
    public record AddValueSensorForMachineCommand(ReadValueSensorForMachineRequest ReadValueSensorForMachine) : IRequest<ErrorOr<Success>>;
    
}
