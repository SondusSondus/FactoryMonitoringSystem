using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Auth.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Commands.GenerateToken
{
    public class GenerateTokenQueryHandler(ITokenGenerator tokenGenerator) : IRequestHandler<GenerateTokenCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

        async Task<ErrorOr<AuthenticationResult>> IRequestHandler<GenerateTokenCommand, ErrorOr<AuthenticationResult>>.Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_tokenGenerator.GenerateToken(cancellationToken));

        }
    }
}
