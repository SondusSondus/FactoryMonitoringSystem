using FactoryMonitoringSystem.Application.Contracts.UserManagement.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Application.Auth.Events.GenerateToken
{
    public class RefreshTokenEventHandler(IUserService userService) : INotificationHandler<RefreshTokenEvent>
    {
        private readonly IUserService _userService = userService;
        public async Task Handle(RefreshTokenEvent notification, CancellationToken cancellationToken)
        {
            await _userService.UpdateRefreshToken(notification.refreshToken, cancellationToken);
        }
    }
}
