using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Factories.Models.Requests;
using MediatR;


namespace FactoryMonitoringSystem.Application.Factories.Commands.CreateFactory
{
    public record CreateFactoryCommand(FactoryRequest FactoryRequet) : IRequest<ErrorOr<Success>>;
}
