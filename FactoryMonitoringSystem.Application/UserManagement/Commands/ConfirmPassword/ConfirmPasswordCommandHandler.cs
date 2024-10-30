using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ConfirmPassword
{
    internal class ConfirmPasswordCommandHandler(IUserService userService) : IRequestHandler<ConfirmPasswordCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(ConfirmPasswordCommand request, CancellationToken cancellationToken)
        {
           return await _userService.ConfirmPassword(request.confirmPassword, cancellationToken);
        }
    }
}
