using ErrorOr;
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
    internal class SensorMachineService : ApplicationService<SensorMachineService, SensorMachine>, ISensorMachineService, IScopedDependency
    {
        public SensorMachineService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        public async Task<ErrorOr<Success>> AddSensorToMachine(SensorMachineResquest sensorMachine, CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Add sensor to amchine");
                var sensorMachineNew = new SensorMachine()
                {
                    MachineId = sensorMachine.MachineId,
                    SensorId = sensorMachine.SensorId,
                };
                WriteRepository.Add(sensorMachineNew);
                await WriteRepository.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating Add sensor to machine");
                return General.Unexpected; // Return a general error in case of failure

            }

        }

        public async Task<ErrorOr<List<SensorMachineResponse>>> GetAllSensorMachine(CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation("Retrieve Factories");

                var factories = await ReadRepository.GetAllIncludeAsync(
                    cancellationToken,
                    (sensorMachine => sensorMachine.Machine), (sensorMachine => sensorMachine.Sensor)
                );
                Logger.LogInformation("Retrieve Factories successfully");
                return factories.Adapt<List<SensorMachineResponse>>();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error when fetch Factories");
                Logger.LogInformation(General.Unexpected.Description);
                return General.Unexpected;
            }

        }
        public async Task<ErrorOr<SensorMachineResponse>> GetSensorMachineById(Guid id, CancellationToken cancellationToken)
        {

            var sensorMachine = await ReadRepository.FindIncludeAsync(cancellationToken,
                sensorMachine => sensorMachine.Id == id,
                (sensorMachine => sensorMachine.Machine), (sensorMachine => sensorMachine.Sensor)
                );

            if (sensorMachine == null)
            {
                Logger.LogError(SensorMachineError.NotFound.Description);
                return SensorMachineError.NotFound; // Return error if factory is not found
            }
            Logger.LogInformation("Sensor machine successfully");
            return sensorMachine.Adapt<SensorMachineResponse>();


        }
    }
}
