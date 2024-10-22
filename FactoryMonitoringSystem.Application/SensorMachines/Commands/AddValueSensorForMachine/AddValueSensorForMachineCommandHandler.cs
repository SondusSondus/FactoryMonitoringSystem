using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Commands.AddValueSensorForMachine
{
    internal class AddValueSensorForMachineCommandHandler(ITrackingSensorMachineValueService trackingSensorMachineValueService) : IRequestHandler<AddValueSensorForMachineCommand, ErrorOr<Success>>
    {
        private readonly ITrackingSensorMachineValueService _trackingSensorMachineValueService=trackingSensorMachineValueService;
        public async Task<ErrorOr<Success>> Handle(AddValueSensorForMachineCommand request, CancellationToken cancellationToken)
        => await _trackingSensorMachineValueService.AddValueSensorForMachine(request.ReadValueSensorForMachine, cancellationToken);
    }
}
