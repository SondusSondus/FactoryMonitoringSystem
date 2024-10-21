using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Machines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Request;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;

namespace FactoryMonitoringSystem.Application.Sensors.Services
{
    public class SensorService : ApplicationService<SensorService, Sensor>, ISensorService, IScopedDependency
    {
        public SensorService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ErrorOr<Success>> CreateSensorAsync(SensorRequest sensorRequest, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Creating Sensor");

            try
            {
                var result = new Sensor(GuidGenerator, sensorRequest.Name, sensorRequest.Type,sensorRequest.MinValue,sensorRequest.MaxValue, sensorRequest.Unit);
                WriteRepository.Add(result);
                await WriteRepository.SaveChangesAsync(cancellationToken);
                Logger.LogInformation("Sensor created successfully: {Name}", result.Name);
                return Result.Success;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating Sensor {Name}", sensorRequest.Name);
                return General.Unexpected; // Return a general error in case of failure

            }
        }

        public async Task<ErrorOr<Success>> DeleteSensorAsync(Guid sensorId, CancellationToken cancellationToken)
        {
            var sensor = await ReadRepository.GetByIdAsync(sensorId, cancellationToken);
            if (sensor == null)
            {
                Logger.LogError(SensorError.NotFound.Description);
                return SensorError.NotFound;
            }
            Logger.LogInformation("Delete sensor {Name} ", sensor.Name);
            WriteRepository.Delete(sensor);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("sensor deleted successfully");
            return Result.Success;
        }

        public async Task<ErrorOr<List<SensorResponse>>> GetAllSensorsAsync(CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Retrieve sensors");
                var sensor = await ReadRepository.GetAllAsync(cancellationToken);
                Logger.LogInformation("Retrieve sensors successfully");
                return sensor.Adapt<List<SensorResponse>>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error when fetch sensors");
                Logger.LogInformation(General.Unexpected.Description);
                return General.Unexpected;
            }
        }

        public async Task<ErrorOr<SensorResponse>> GetSensorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var sensor = await ReadRepository.GetByIdAsync(id, cancellationToken);
            if (sensor == null)
            {
                Logger.LogError(SensorError.NotFound.Description);
                return SensorError.NotFound; // Return error if factory is not found
            }
            Logger.LogInformation("Fetch sensor {name}", sensor.Name);
            Logger.LogInformation("Fetch sensor successfully");
            return sensor.Adapt<SensorResponse>();
        }

        public Task<ErrorOr<List<SensorResponse>>> GetSensorsByMachineIdAsync(Guid machineId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<Success>> UpdateSensorAsync(UpdateSensorRequest sensorRequest, CancellationToken cancellationToken)
        {
            var sensor = await ReadRepository.GetByIdAsync(sensorRequest.Id, cancellationToken);
            if (sensor == null)
            {
                Logger.LogError(SensorError.NotFound.Description);
                return SensorError.NotFound;
            }
            Logger.LogInformation("Sensor factory {Sensor} ", sensorRequest.Name);
            sensor.Name = sensorRequest.Name;
            sensor.Type = sensorRequest.Type;
            sensor.Unit = sensorRequest.Unit;
            WriteRepository.Update(sensor);
            await WriteRepository.SaveChangesAsync(cancellationToken);
            Logger.LogInformation("Sensor updated successfully");
            return Result.Success;
        }

        public Task<ErrorOr<Success>> UpdateSensorValueAsync(Guid sensorId, double newValue, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
