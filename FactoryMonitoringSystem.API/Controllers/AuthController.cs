using ErrorOr;
using FactoryMonitoringSystem.Application.Auth.Commands.GenerateToken;
using FactoryMonitoringSystem.Application.Auth.Commands.Login;
using FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator;
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand query, CancellationToken cancellationToken)
        {

            var authResult = await Mediator.Send(query, cancellationToken);

            //// Set the Access Token and Refresh Token in HTTP-Only cookies
            if (!authResult.IsError)
            {
                SetTokenCookie("AccessToken", authResult.Value.AuthenticationResult.AccessToken, _jwtSettings.AccessTokenExpirationMinutes);
                SetTokenCookie("RefreshToken", authResult.Value.AuthenticationResult.AccessToken, _jwtSettings.RefreshTokenExpirationDays);

            }

            return authResult.Match(
                   authResult => Ok(authResult.LoginResponse),
                   Problem);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(CancellationToken cancellationToken)
        {
            var refreshToken = Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Problem(new List<Error> { Error.Unauthorized("Refresh token not found.") });
            }
            if (CurrentUser == null || CurrentUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Problem(new List<Error> { Error.Unauthorized("Invalid or expired refresh token.") });
            }

            var token = await Mediator.Send(new GenerateTokenCommand(), cancellationToken);

            // Generate new access token and refresh token
            var newAccessToken = token.Value.AccessToken;
            var newRefreshToken = token.Value.AccessToken;
            if (!token.IsError)
            {
                // Set new tokens in cookies
                SetTokenCookie("AccessToken", newAccessToken, _jwtSettings.AccessTokenExpirationMinutes);
                SetTokenCookie("RefreshToken", newRefreshToken, _jwtSettings.RefreshTokenExpirationDays);
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



    }
}
