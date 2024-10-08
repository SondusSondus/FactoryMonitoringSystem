

using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler(IUserService userService) : IRequestHandler<ForgotPasswordCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ForgotPasswordByEmail(request.email, cancellationToken);
        }
    }
}
