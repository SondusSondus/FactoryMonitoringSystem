using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler(IUserService userService) : IRequestHandler<ChangePasswordCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ChangePasswordAsync(request.changePassword,cancellationToken);
        }
    }
}
