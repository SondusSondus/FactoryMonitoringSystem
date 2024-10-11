using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetAllSensors
{
    internal class GetAllSensorsQueryHandler(ISensorService sensorService) : IRequestHandler<GetAllSensorsQuery, ErrorOr<List<SensorResponse>>>
    {
        private readonly ISensorService _sensorService = sensorService;
        public async Task<ErrorOr<List<SensorResponse>>> Handle(GetAllSensorsQuery request, CancellationToken cancellationToken)
                => await _sensorService.GetAllSensorsAsync(cancellationToken);
    }
}
