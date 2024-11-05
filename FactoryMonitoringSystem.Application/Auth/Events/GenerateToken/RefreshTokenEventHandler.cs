using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;


namespace FactoryMonitoringSystem.Application.Auth.Events.GenerateToken
{
    internal class RefreshTokenEventHandler(IUserService userService) : INotificationHandler<RefreshTokenEvent>
    {
        private readonly IUserService _userService = userService;
        public async Task Handle(RefreshTokenEvent notification, CancellationToken cancellationToken)
          => await _userService.UpdateRefreshToken(notification.refreshToken, cancellationToken);

    }
}
