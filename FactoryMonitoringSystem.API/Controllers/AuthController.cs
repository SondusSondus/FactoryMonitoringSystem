using ErrorOr;
using FactoryMonitoringSystem.Application.Auth.Commands.CheckUserByRefrshToken;
using FactoryMonitoringSystem.Application.Auth.Commands.GenerateToken;
using FactoryMonitoringSystem.Application.Auth.Commands.InvalidateRefreshToken;
using FactoryMonitoringSystem.Application.Auth.Commands.Login;
using FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator;
using FactoryMonitoringSystem.Shared.Utilities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FactoryMonitoringSystem.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly JwtSettings _jwtSettings;

        public AuthController(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand query, CancellationToken cancellationToken)
        {

            var authResult = await Mediator.Send(query, cancellationToken);

            //// Set the Access Token and Refresh Token in HTTP-Only cookies
            if (!authResult.IsError)
            {
                SetTokenCookie("AccessToken", authResult.Value.AuthenticationResult.AccessToken, _jwtSettings.AccessTokenExpirationMinutes);
                SetTokenCookie("RefreshToken", authResult.Value.AuthenticationResult.RefreshToken, _jwtSettings.RefreshTokenExpirationDays * 24 * 60);

            }

            return authResult.Match(
                   authResult => Ok(authResult.LoginResponse),
                   Problem);
        }

        [HttpPost("Logout")]
        [Authorize(policy: Policy.User)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            // Remove the JWT token from cookies
            if (Request.Cookies["AccessToken"] != null)
            {
                RemoveTokenCookie("AccessToken");
            }

            if (Request.Cookies["RefreshToken"] != null)
            {
                RemoveTokenCookie("RefreshToken");
            }

            if (Request.Cookies["RefreshTokenExpiryTime"] != null)
            {
                RemoveTokenCookie("RefreshTokenExpiryTime");
            }

            var authResult = await Mediator.Send(new InvalidateRefreshTokenCommand(), cancellationToken);
            return authResult.Match(
                authResult => Ok(),
                Problem);

        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]

        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Problem(new List<Error> { Error.Unauthorized("Refresh token not found.") });
            }
            var refreshTokenExpiryTime = await Mediator.Send(new CheckUserByRefrshTokenCommand(refreshToken), cancellationToken);
            if (refreshTokenExpiryTime.IsError || refreshTokenExpiryTime.Value == DateTime.MinValue || refreshTokenExpiryTime.Value <= DateTime.UtcNow)
            {
                return Problem(new List<Error> { Error.Unauthorized("Invalid or expired refresh token.") });
            }

            var token = await Mediator.Send(new GenerateTokenCommand(), cancellationToken);

            if (!token.IsError)
            {
                // Set new tokens in cookies
                SetTokenCookie("AccessToken", token.Value.AccessToken, _jwtSettings.AccessTokenExpirationMinutes);
                SetTokenCookie("RefreshToken", token.Value.AccessToken, _jwtSettings.RefreshTokenExpirationDays * 24 * 60);

            }

            return token.Match(
                         token => Ok(),
                         Problem);
        }

        private void SetTokenCookie(string cookieName, string token, double expirationMinutes)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Use Secure flag to send cookies over HTTPS only
                SameSite = SameSiteMode.Strict, // Prevents CSRF attacks
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes)
            };
            Response.Cookies.Append(cookieName, token, cookieOptions);
        }

        private void RemoveTokenCookie(string cookieName)
        {

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1), // Expire the cookie immediately
                HttpOnly = true,
                Secure = true, // Should be true in production with HTTPS
                SameSite = SameSiteMode.Strict
            };
            Response.Cookies.Append(cookieName, "", cookieOptions);

        }



    }
}
