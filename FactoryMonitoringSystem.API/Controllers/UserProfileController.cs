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
    [Authorize(Roles= Roles.User)]

    public class UserProfileController : ApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetProfileUserQuery(), cancellationToken);
            return result.Match(
                result => Ok(result),
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
        // POST: api/userprofile/changepassword
        [HttpPost("ChangePassword")]
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
