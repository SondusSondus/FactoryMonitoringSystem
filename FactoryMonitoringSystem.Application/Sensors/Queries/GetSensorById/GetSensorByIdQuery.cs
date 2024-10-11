using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Sensors.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Sensors.Queries.GetSensorById
{
    public record GetSensorByIdQuery(Guid SensorId) : IRequest<ErrorOr<SensorResponse>>;

}
