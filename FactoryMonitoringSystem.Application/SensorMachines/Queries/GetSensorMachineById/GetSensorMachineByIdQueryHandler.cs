using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.GetSensorMachineById
{
    internal class GetSensorMachineByIdQueryHandler(ISensorMachineService sensorMachineService) : IRequestHandler<GetSensorMachineByIdQuery, ErrorOr<SensorMachineResponse>>
    {
        private readonly ISensorMachineService _sensorMachineService = sensorMachineService;
        public async Task<ErrorOr<SensorMachineResponse>> Handle(GetSensorMachineByIdQuery request, CancellationToken cancellationToken)
          => await _sensorMachineService.GetSensorMachineById(request.Id, cancellationToken);
    }
}
