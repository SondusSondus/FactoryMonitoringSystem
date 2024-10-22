using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueForSensorMachine;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.ReadValueSensorForMachine
{
    internal class ReadValueSensorForMachineByIdQueryHandler(ITrackingSensorMachineValueService trackingSensorMachineValueService) : IRequestHandler<ReadValueSensorForMachineByIdQuery, ErrorOr<List<TrackingSensorMachineValueServiceResponse>>>
    {
        private readonly ITrackingSensorMachineValueService _trackingSensorMachineValueService = trackingSensorMachineValueService;
        async Task<ErrorOr<List<TrackingSensorMachineValueServiceResponse>>> IRequestHandler<ReadValueSensorForMachineByIdQuery, ErrorOr<List<TrackingSensorMachineValueServiceResponse>>>.Handle(ReadValueSensorForMachineByIdQuery request, CancellationToken cancellationToken)
           => await _trackingSensorMachineValueService.GetSensorMachineValueById(request.sensorMachineId, cancellationToken);
    }
}
