using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.GetSensorMachineById
{
    public record GetSensorMachineByIdQuery(Guid Id) :IRequest<ErrorOr<SensorMachineResponse>>;
 
}
