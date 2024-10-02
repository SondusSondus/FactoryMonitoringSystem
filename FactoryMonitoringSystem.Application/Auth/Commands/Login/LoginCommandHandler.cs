using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Auth.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Commands.Login
{
    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
    {
        private readonly IAuthService _authService = authService;

        public async Task<ErrorOr<LoginResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.AuthenticateAsync(request.loginRequest, cancellationToken);
        }


    }
}
