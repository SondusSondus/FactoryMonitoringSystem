using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Commands.UpdateFactor
{
    public record UpdateFactoryCommand(UpdateFactoryRequest FactoryRequet) : IRequest<ErrorOr<Success>>;

}
