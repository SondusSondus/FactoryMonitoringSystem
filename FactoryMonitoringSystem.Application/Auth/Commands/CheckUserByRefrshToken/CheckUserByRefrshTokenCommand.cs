using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Auth.Commands.CheckUserByRefrshToken
{
    public record CheckUserByRefrshTokenCommand(string RefrshToken) : IRequest<ErrorOr<DateTime>>;
}
