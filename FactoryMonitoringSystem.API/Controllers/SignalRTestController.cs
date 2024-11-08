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
        private readonly IHubContext<MonitoringHub> _hubContext;

        public SignalRTestController(IHubContext<MonitoringHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // Endpoint to send a message to a specific group
        [HttpPost("send-to-group")]
        public async Task<IActionResult> SendToGroup([FromQuery] string groupName, [FromBody] string message)
        {
            await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", message);
            return Ok(new { Status = "Message sent to group", Group = groupName });
        }

        // Endpoint to send a message to a specific user
        [HttpPost("send-to-user")]
        public async Task<IActionResult> SendToUser([FromQuery] string userId, [FromBody] string message)
        {
            await _hubContext.Clients.User(userId).SendAsync("ReceiveMessage", message);
            return Ok(new { Status = "Message sent to user", UserId = userId });
        }

        // Endpoint to broadcast a message to all clients
        [HttpPost("notify-all")]
        public async Task<IActionResult> NotifyAll([FromBody] string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            return Ok(new { Status = "Message broadcasted to all clients" });
        }
    }
}
