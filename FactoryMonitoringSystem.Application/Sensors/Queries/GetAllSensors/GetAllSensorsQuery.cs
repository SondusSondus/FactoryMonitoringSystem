using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetAllSensors
{
    public record GetAllSensorsQuery() : IRequest<ErrorOr<List<SensorResponse>>>;

}
