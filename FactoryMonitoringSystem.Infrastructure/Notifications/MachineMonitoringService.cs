using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using FactoryMonitoringSystem.Domain.SensorMachines.Views;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Domain.Shared.Machine.Dtos;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{

    public class MachineMonitoringService
    {
        private readonly IHubContext<NotificationHub,INotificationClient> _hubContext;
        private readonly IReadRepository<SensorValueOutOfRangeView> _sensorValueOutOfRangeView;
        private readonly ILogger<MachineMonitoringService> _logger;

        public MachineMonitoringService(
        IHubContext<NotificationHub, INotificationClient> hubContext,
            IReadRepository<SensorValueOutOfRangeView> sensorValueOutOfRangeView,
            ILogger<MachineMonitoringService> logger)
        {
            _hubContext = hubContext;
            _sensorValueOutOfRangeView = sensorValueOutOfRangeView;
            _logger = logger;
        }

        public async Task CheckSensaorMachineValueAsync()
        {
            var sensorMachines = await _sensorValueOutOfRangeView.GetAllAsync();

            foreach (var sensorMachine in sensorMachines)
            {

                var failureDto = new MachineSensorFailureDto
                {
                    SensorName = sensorMachine.SensorName,
                    MachineName = sensorMachine.MachineName,
                    Value = sensorMachine.Value,
                    ErrorMessage = $"Value {sensorMachine.Value} of sensor {sensorMachine.SensorName} for " +
                    $"machine {sensorMachine.MachineName} . Reading conditions are not met." +
                    $" A problem may occur. Please track the problem as soon as possible.",
                    Severity = "Critical",
                    Timestamp = DateTime.UtcNow
                };
                _logger.LogInformation($"failed: {failureDto.ErrorMessage}");
                // Broadcast the machine failure alert
                await _hubContext.Clients.Group(RolesEnum.Operator.ToString()).ReceiveMessage(JsonConvert.SerializeObject(failureDto));
                await _hubContext.Clients.Group(RolesEnum.Admin.ToString()).ReceiveMessage(JsonConvert.SerializeObject(failureDto));

            }
        }

    }

}
