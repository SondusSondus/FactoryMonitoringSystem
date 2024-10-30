using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.UserManagement.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService =userService;
        async Task<ErrorOr<Success>> IRequestHandler<UpdateUserCommand, ErrorOr<Success>>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserProfile(request.UpdateUser, cancellationToken);
        }
    }
}
