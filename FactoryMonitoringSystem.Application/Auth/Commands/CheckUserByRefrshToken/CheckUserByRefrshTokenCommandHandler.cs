using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.Auth.Commands.CheckUserByRefrshToken
{
    internal class CheckUserByRefrshTokenCommandHandler(IUserService userService) : IRequestHandler<CheckUserByRefrshTokenCommand, ErrorOr<DateTime>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<DateTime>> Handle(CheckUserByRefrshTokenCommand request, CancellationToken cancellationToken)
            => await _userService.CheckUserByRefreshToken(request.RefrshToken, cancellationToken);
    }
}
