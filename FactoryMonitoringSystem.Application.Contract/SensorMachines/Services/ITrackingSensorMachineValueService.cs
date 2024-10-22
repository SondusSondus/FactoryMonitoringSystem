using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services
{
    public interface ITrackingSensorMachineValueService
    {
        Task<ErrorOr<Success>> AddValueSensorForMachine(ReadValueSensorForMachineRequest readValueSensorForMachine, CancellationToken cancellationToken);
        Task<ErrorOr<List<TrackingSensorMachineValueServiceResponse>>> GetSensorMachineValueById(Guid sensorMachineId, CancellationToken cancellationToken);
    }
}
