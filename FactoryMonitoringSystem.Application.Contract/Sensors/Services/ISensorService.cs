using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;

namespace FactoryMonitoringSystem.Application.Contracts.Sensors.Services
{
    public interface ISensorService
    {
        Task<ErrorOr<Success>> CreateSensorAsync(SensorRequest sensorRequest, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateSensorAsync(UpdateSensorRequest sensorRequest, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> UpdateSensorValueAsync(Guid sensorId, double newValue, CancellationToken cancellationToken);
        Task<ErrorOr<Success>> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken);
        Task<ErrorOr<List<SensorResponse>>> GetSensorsByMachineIdAsync(Guid machineId, CancellationToken cancellationToken);
        Task<ErrorOr<SensorResponse>> GetSensorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ErrorOr<List<SensorResponse>>> GetAllSensorsAsync(CancellationToken cancellationToken);
    }
}
