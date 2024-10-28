using FactoryMonitoringSystem.Application.UserManagement.Commands.DeleteUser;
using FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers;
using FactoryMonitoringSystem.Application.UserManagement.Commands.ResetPasswordUser;
using FactoryMonitoringSystem.Application.UserManagement.Commands.UnlockedUser;
using FactoryMonitoringSystem.Application.UserManagement.Queries.GetUserById;
using FactoryMonitoringSystem.Application.UserManagement.Queries.GetUsers;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryMonitoringSystem.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]

    public class UserController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);
            return result.Match(
                result => Ok(result),
                Problem
                );
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUser([FromQuery] GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return result.Match(
                result => Ok(result),
                Problem
                );
        } 
        
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult>  CreateUser([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                  result => Ok(),
                  Problem);

        }

        [HttpPost("UnlockedUser")]
        public async Task<IActionResult> UnlockedUser([FromQuery] UnlockedUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );
        }

        [HttpPost("id")]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                result => Ok(),
                Problem
                );
        }
    }
}
