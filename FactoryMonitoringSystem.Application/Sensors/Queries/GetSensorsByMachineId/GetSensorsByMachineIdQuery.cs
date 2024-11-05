using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorsByMachineId
{
    public record GetSensorsByMachineIdQuery : IRequest<List<SensorResponse>>;
}
