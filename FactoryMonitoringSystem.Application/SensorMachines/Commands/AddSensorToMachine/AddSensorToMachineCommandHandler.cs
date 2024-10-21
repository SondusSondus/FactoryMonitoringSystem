using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddSensorToMachine
{
    internal class AddSensorToMachineCommandHandler(ISensorMachineService sensorMachineService) : IRequestHandler<AddSensorToMachineCommand, ErrorOr<Success>>
    {
        private readonly ISensorMachineService _sensorMachineService = sensorMachineService;
        public async Task<ErrorOr<Success>> Handle(AddSensorToMachineCommand request, CancellationToken cancellationToken)
        => await _sensorMachineService.AddSensorToMachine(request.SensorMachine, cancellationToken);
    }
}
