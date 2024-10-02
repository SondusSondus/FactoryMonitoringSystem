using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Requests;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Commands.Login
{
    public record LoginCommand(LoginRequest loginRequest) : IRequest<ErrorOr<LoginResult>>;



}
