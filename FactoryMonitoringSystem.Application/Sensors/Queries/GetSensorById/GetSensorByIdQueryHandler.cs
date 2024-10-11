using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorById
{
    internal class GetSensorByIdQueryHandler(ISensorService sensorService) : IRequestHandler<GetSensorByIdQuery, ErrorOr<SensorResponse>>
    {
        private readonly ISensorService _sensorService = sensorService;
        public async Task<ErrorOr<SensorResponse>> Handle(GetSensorByIdQuery request, CancellationToken cancellationToken)
           => await _sensorService.GetSensorByIdAsync(request.SensorId, cancellationToken);
    }
}
