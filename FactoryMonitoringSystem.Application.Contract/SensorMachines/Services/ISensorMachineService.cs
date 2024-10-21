using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services
{
    public interface ISensorMachineService
    {
        Task<ErrorOr<Success>> AddSensorToMachine(SensorMachineResquest sensorMachine, CancellationToken cancellationToken);
        Task<ErrorOr<List<SensorMachineResponse>>> GetAllSensorMachine(CancellationToken cancellationToken);
        Task<ErrorOr<SensorMachineResponse>> GetSensorMachineById(Guid id, CancellationToken cancellationToken);
    }
}
