using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.SensorMachines.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.SensorMachines.Queries.GetAllSensorMachine
{
    public record GetAllSensorMachineQuery : IRequest<ErrorOr<List<SensorMachineResponse>>>;

}
