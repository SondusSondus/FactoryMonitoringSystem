using ErrorOr;
using FactoryMonitoringSystem.Application.Auth.Events.GenerateToken;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Auth.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Commands.GenerateToken
{
    public class GenerateTokenQueryHandler(ITokenGenerator tokenGenerator, IMediator mediator) : IRequestHandler<GenerateTokenCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
        private readonly IMediator _mediator = mediator;

        async Task<ErrorOr<AuthenticationResult>> IRequestHandler<GenerateTokenCommand, ErrorOr<AuthenticationResult>>.Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var result = _tokenGenerator.GenerateToken(cancellationToken);
            await _mediator.Publish(new RefreshTokenEvent(result.Value.RefreshToken), cancellationToken);
            return await Task.FromResult(result);

        }
    }
}
