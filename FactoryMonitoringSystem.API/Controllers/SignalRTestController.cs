using FactoryMonitoringSystem.Infrastructure.Notifications;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FactoryMonitoringSystem.Api.Controllers
{

    [ApiController]
    [Route("api/signalr")]
    [Authorize(Roles = Roles.Admin)]

    public class SignalRTestController : ControllerBase
    {
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public SignalRTestController(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        // Endpoint to send a message to a specific group
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser([FromQuery] string groupName, [FromBody] string message)
        {
            await _hubContext.Clients.User(groupName).ReceiveMessage( message);
            return Ok(new { Status = "Message sent to group", Group = groupName });
        }
        
        // Endpoint to send a message to a specific group
        [HttpPost("SendToGroup")]
        public async Task<IActionResult> SendToGroup([FromQuery] string groupName, [FromBody] string message)
        {
            await _hubContext.Clients.Group(groupName).ReceiveMessage( message);
            return Ok(new { Status = "Message sent to group", Group = groupName });
        }

        // Endpoint to send a message to a specific user
        [HttpPost("SendToUser")]
        public async Task<IActionResult> SendToUser([FromQuery] string userId, [FromBody] string message)
        {
            await _hubContext.Clients.User(userId).ReceiveMessage(message);
            return Ok(new { Status = "Message sent to user", UserId = userId });
        }

        // Endpoint to broadcast a message to all clients
        [HttpPost("NotifyAll")]
        public async Task<IActionResult> NotifyAll([FromBody] string message)
        {
            await _hubContext.Clients.All.ReceiveMessage(message);
            return Ok(new { Status = "Message broadcasted to all clients" });
        }
    }
}
