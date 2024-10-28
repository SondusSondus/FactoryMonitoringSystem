using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Factories.Commands.DeleteFactory
{
    public record DeleteFactoryCommand(Guid id) : IRequest<ErrorOr<Success>>;

}
