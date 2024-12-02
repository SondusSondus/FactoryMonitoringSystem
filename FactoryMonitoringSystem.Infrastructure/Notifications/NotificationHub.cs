using FactoryMonitoringSystem.Shared;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;


namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    [Authorize] // Ensure only authenticated users can connect
    public class NotificationHub : Hub<INotificationClient>
    {
        private static readonly HashSet<string> AllowedGroups = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            RolesEnum.Operator.ToString(),
            RolesEnum.Admin.ToString()
        };

        public override async Task OnConnectedAsync()
        {
            //Handle user joining groups if needed, e.g., based on role
            var group = Context.User?.FindFirst(ClaimTypes.Role)?.Value;
            var email = Context.User?.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(group) && (group == RolesEnum.Operator.ToString() || group == RolesEnum.Admin.ToString()))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
            }
            await Clients.Clients(Context.ConnectionId).ReceiveMessage($"Welcome {Context.UserIdentifier} to the Monitoring Hub!");

            await base.OnConnectedAsync();
        }
    }

}
public interface INotificationClient
{
    Task ReceiveMessage(string message);
}
