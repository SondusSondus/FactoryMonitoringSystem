using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using FactoryMonitoringSystem.Domain.SensorMachine.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Domain.Shared.Machine.Dtos;
using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{

    public class MachineMonitoringService
    {
        private readonly IHubContext<MonitoringHub> _hubContext;
        private readonly IReadRepository<SensorMachine> _sensorMachineRepository;
        private readonly IReadRepository<Sensor> _sensorRepository;
        private readonly ILogger<MachineMonitoringService> _logger;

        public MachineMonitoringService(
            IHubContext<MonitoringHub> hubContext,
            IReadRepository<SensorMachine> sensorMachineRepository,
            IReadRepository<Sensor> sensorRepository,
            ILogger<MachineMonitoringService> logger)
        {
            _hubContext = hubContext;
            _sensorMachineRepository = sensorMachineRepository;
            _sensorRepository = sensorRepository;
            _logger = logger;
        }

        public async Task CheckMachineStatusAsync()
        {
            CancellationToken cancellation = CancellationToken.None;
            var sensorMachines = await _sensorMachineRepository.GetAllIncludeAsync(cancellation, sensorMachine => sensorMachine.machine , sensorMachine => sensorMachine.Sensor);

            foreach (var sensorMachine in sensorMachines)
            {
                if (sensorMachine.Sensor.MinValue <= 0) // Example failure logic
                {
                    var failureDto = new MachineFailureDto
                    {
                        MachineId = sensorMachine.machineId.ToString(),
                        MachineName = sensorMachine.machine.Name,
                        ErrorMessage = "Machine stopped unexpectedly",
                        Severity = "Critical",
                        Timestamp = DateTime.UtcNow
                    };
                    _logger.LogInformation($"Machine {failureDto.MachineName} failed: {failureDto.ErrorMessage}");
                    // Broadcast the machine failure alert
                    await _hubContext.Clients.Group(RolesEnum.Operator.ToString()).SendAsync("ReceiveMachineFailure", failureDto);
                    await _hubContext.Clients.Group(RolesEnum.Admin.ToString()).SendAsync("ReceiveMachineFailure", failureDto);
                }
            }
        }
       
    }

}
