using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.SignalR;


namespace FactoryMonitoringSystem.Infrastructure.Notifications
{
    public class NotificationHub : Hub ,ITransientDependency
    {
        // This method can be called from your server to notify clients
        public async Task SendNotificationToUser(string userId, string message)
        {
            // Send a message to a specific client using their connection ID
            await Clients.User(userId).SendAsync("ReceivedNotification", message);
        }

        // Optionally, handle connection events
        public override Task OnConnectedAsync()
        {
            // Log connection or perform other actions
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Handle disconnection
            return base.OnDisconnectedAsync(exception);
        }
    }
}