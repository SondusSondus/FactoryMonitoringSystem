using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.ResetPasswordUser
{
    internal class ResetPasswordUserCommandHandler(IUserService userService) : IRequestHandler<ResetPasswordUserCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(ResetPasswordUserCommand request, CancellationToken cancellationToken)
        => await _userService.ResetPasswordUser(request.Id, cancellationToken);
    }
}
