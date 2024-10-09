using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor
{
    public record UpdateFactoryCommand(FactoryRequet FactoryRequet) : IRequest<ErrorOr<Success>>;

}
