using FactoryMonitoringSystem.Application.UserManagement.Commands.ChangePassword;
using FactoryMonitoringSystem.Application.UserManagement.Commands.UpdateUser;
using FactoryMonitoringSystem.Application.UserManagement.Queries.GetProfileUser;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryMonitoringSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(policy: Roles.User)]
    public class UserProfileController : ApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetProfile(GetProfileUserQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );

        }
        // POST: api/userprofile/change-password
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );
        }
    }
}
