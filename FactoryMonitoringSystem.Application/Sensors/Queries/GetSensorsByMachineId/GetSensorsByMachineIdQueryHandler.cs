using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorsByMachineId
{
    internal class GetSensorsByMachineIdQueryHandler : IRequestHandler<GetSensorsByMachineIdQuery, List<SensorResponse>>
    {
        public Task<List<SensorResponse>> Handle(GetSensorsByMachineIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
