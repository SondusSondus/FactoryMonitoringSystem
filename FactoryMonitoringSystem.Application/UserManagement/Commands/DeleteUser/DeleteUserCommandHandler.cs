using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;

namespace FactoryMonitoringSystem.Application.UserManagement.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
                  => await _userService.DeleteUser(request.id, cancellationToken);
    }
}
