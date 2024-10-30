using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Common.Services;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using FactoryMonitoringSystem.Shared;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;

namespace FactoryMonitoringSystem.Application.SensorMachines.Services
{
    internal class TrackingSensorMachineValueService : ApplicationService<TrackingSensorMachineValueService, TrackingSensorMachineValue>, ITrackingSensorMachineValueService, IScopedDependency
    {
        public TrackingSensorMachineValueService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ErrorOr<Success>> AddValueSensorForMachine(ReadValueSensorForMachineRequest readValueSensorForMachine, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Add tracking value sensor for machine");
            try
            {
                var addTrackingValue = new TrackingSensorMachineValue
                {
                    SensorMachineId = readValueSensorForMachine.SensorMachineId,
                    Value = readValueSensorForMachine.Value,
                    DateOfReadingValue = DateTime.UtcNow,
                };

                WriteRepository.Add(addTrackingValue);
                await WriteRepository.SaveChangesAsync(cancellationToken);
                Logger.LogInformation("Add tracking value sensor for machine successfully");
                return Result.Success;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating Add sensor to machine");
                return General.Unexpected; // Return a general error in case of failure

            }
        }

        public async Task<ErrorOr<List<TrackingSensorMachineValueServiceResponse>>> GetSensorMachineValueById(Guid sensorMachineId, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Get tracking value sensor for machine");
            var trackingValue = await ReadRepository.GetAllAsync(cancellationToken, track => track.SensorMachineId == sensorMachineId);
            if (!trackingValue.Any())
            {
                Logger.LogError(SensorMachineError.NotFound.Description);
                return SensorMachineError.NotFound; // Return error if factory is not found
            }
            Logger.LogInformation("Retrieve tracking value sensor for machine successfully");
            return trackingValue.Adapt<List<TrackingSensorMachineValueServiceResponse>>();
        }

      
    }
}
