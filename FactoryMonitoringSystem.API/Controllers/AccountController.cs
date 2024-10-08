using FactoryMonitoringSystem.Application.UserManagement.Commands.ConfirmPassword;
using FactoryMonitoringSystem.Application.UserManagement.Commands.ForgotPassword;
using FactoryMonitoringSystem.Application.UserManagement.Commands.RegistrationUsers;
using FactoryMonitoringSystem.Application.UserManagement.Commands.VerifyEmail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FactoryMonitoringSystem.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]

    public class AccountController : ApiController
    {
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
        {

            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                  result => Ok(),
                  Problem);

        }

        [HttpPost("VerifyEmail")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                 result => Ok(),
                 Problem);
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                 result => Ok(),
                 Problem);

        } 
        [HttpPost("ConfirmPassword")]
        public async Task<IActionResult> ConfirmPassword([FromBody] ConfirmPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return result.Match(
                 result => Ok(),
                 Problem);

        }
    }
}
