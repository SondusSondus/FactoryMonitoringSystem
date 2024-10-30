using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail
{
    internal class VerifyEmailCommandHandler(IUserService userService) : IRequestHandler<VerifyEmailCommand, ErrorOr<Success>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ErrorOr<Success>> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            return await _userService.VerifyEmailAsync(request.VerifyEmailRequest, cancellationToken);
        }
    }
}
