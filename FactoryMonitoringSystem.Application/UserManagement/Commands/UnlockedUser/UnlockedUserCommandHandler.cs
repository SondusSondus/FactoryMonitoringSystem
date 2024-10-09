using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UnlockedUser
{
    internal class UnlockedUserCommandHandler(IUserService userService) : IRequestHandler<UnlockedUserCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(UnlockedUserCommand request, CancellationToken cancellationToken)
        => await _userService.UnlockedUser(request.Id,cancellationToken);

    }
}
