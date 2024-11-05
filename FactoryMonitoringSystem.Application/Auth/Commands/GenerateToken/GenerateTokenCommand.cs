using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using MediatR;

namespace FactoryMonitoringSystem.Application.Auth.Commands.GenerateToken
{
    public record GenerateTokenCommand() : IRequest<ErrorOr<AuthenticationResult>>;
}
