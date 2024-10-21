using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.GetAllSensorMachine
{
    internal class GetAllSensorMachineQueryHandler(ISensorMachineService sensorMachineService) : IRequestHandler<GetAllSensorMachineQuery, ErrorOr<List<SensorMachineResponse>>>
    {
        private readonly ISensorMachineService _sensorMachineService = sensorMachineService;
        public async Task<ErrorOr<List<SensorMachineResponse>>> Handle(GetAllSensorMachineQuery request, CancellationToken cancellationToken)
            => await _sensorMachineService.GetAllSensorMachine(cancellationToken);
    }
}
