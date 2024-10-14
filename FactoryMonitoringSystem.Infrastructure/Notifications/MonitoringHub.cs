using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;


namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    [Authorize] // Ensure only authenticated users can connect
    public class MonitoringHub : Hub, ITransientDependency
    {
        public override async Task OnConnectedAsync()
        {
            // Handle user joining groups if needed, e.g., based on role
            var role = Context.User?.FindFirst("role")?.Value;
            var userID = Context.User?.FindFirst("id")?.Value;
            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(userID))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, role);
                await Groups.AddToGroupAsync(userID, role);
            }
            await base.OnConnectedAsync();
        }
    }

}

