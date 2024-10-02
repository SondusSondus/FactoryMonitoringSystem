using FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers;
using FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace FactoryMonitoringSystem.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ApiController
    {
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                  result => Ok(),
                  Problem);

        }

        [HttpPost("verify-email")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                 result => Ok(),
                 Problem);

        }
    }
}
