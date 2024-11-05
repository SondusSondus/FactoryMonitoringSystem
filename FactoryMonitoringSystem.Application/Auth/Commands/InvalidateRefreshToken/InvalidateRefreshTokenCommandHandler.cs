using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Commands.InvalidateRefreshToken
{
    internal class InvalidateRefreshTokenCommandHandler(IUserService userService) : IRequestHandler<InvalidateRefreshTokenCommand,ErrorOr<Success>>
    {
        private readonly IUserService _userService=  userService;
        public async Task<ErrorOr<Success>> Handle(InvalidateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateRefreshTokenToInValid(cancellationToken);
        }
    }
}
