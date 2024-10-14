using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.SignalR;

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public class ManageUserService
    {
        private readonly IHubContext<MonitoringHub> _hubContext;
        private readonly CurrentUser _currentUser;

        public ManageUserService(IHubContext<MonitoringHub> hubContext, CurrentUser currentUser)
        {
            _hubContext = hubContext;
            _currentUser = currentUser;
        }

        public async Task AssignUserToRoleGroup()
        {
            await _hubContext.Groups.RemoveFromGroupAsync(_currentUser.Id.ToString(), _currentUser.Role);
            await _hubContext.Groups.AddToGroupAsync(_currentUser.Id.ToString(), _currentUser.Role);
        }


    }
}
