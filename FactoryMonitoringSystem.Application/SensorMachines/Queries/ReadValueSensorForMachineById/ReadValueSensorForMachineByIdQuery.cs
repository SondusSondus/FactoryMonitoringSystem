using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueForSensorMachine
{
    public record ReadValueSensorForMachineByIdQuery(Guid sensorMachineId) : IRequest<ErrorOr<List<TrackingSensorMachineValueServiceResponse>>>;

}
