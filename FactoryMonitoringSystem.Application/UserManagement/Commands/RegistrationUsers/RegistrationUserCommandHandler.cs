using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers
{
    internal class RegistrationUserCommandHandler(IUserService userService) : IRequestHandler<RegistrationUserCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RegisterUserAsync(request.SingUpRequest,cancellationToken);
        }
    }
}
