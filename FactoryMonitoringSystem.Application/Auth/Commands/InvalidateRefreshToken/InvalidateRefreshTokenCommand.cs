using ErrorOr;
using MediatR;

namespace FactoryMonitoringSystem.Application.Auth.Commands.InvalidateRefreshToken
{
    public record InvalidateRefreshTokenCommand() : IRequest<ErrorOr<Success>>;

}
