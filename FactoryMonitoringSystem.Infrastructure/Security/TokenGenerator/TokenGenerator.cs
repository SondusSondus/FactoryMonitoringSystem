using ErrorOr;
using FactoryMonitoringSystem.Application.Contracts.Auth.Models.Responses;
using FactoryMonitoringSystem.Application.Contracts.Auth.Services;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider;
using FactoryMonitoringSystem.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static FactoryMonitoringSystem.Shared.Utilities.Constant.Errors;

namespace FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator
{
    public class TokenGenerator(IOptions<JwtSettings> jwtOptions, ICurrentUserProvider currentUserProvider) : ITokenGenerator, IScopedDependency
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;
        private readonly ICurrentUserProvider _currentUserProvider = currentUserProvider;

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }

        }
        private string GenerateAccessToken(string id, string userName, string email, string role)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, userName),
                new(ClaimTypes.Email, email ),
                new("id",  id),
                new("expiryTime", DateTime.UtcNow.ToString()),
                new(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public ErrorOr<AuthenticationResult> GenerateToken(CancellationToken cancellationToken)
        {
            try
            {
                return new AuthenticationResult(GenerateAccessToken(_currentUserProvider.GetCurrentUser().Id.ToString(), _currentUserProvider.GetCurrentUser().Username,
                        _currentUserProvider.GetCurrentUser().Email, _currentUserProvider.GetCurrentUser().Role), GenerateRefreshToken());
            }
            catch (Exception )
            {
                return General.TokenGeneratorFailure;
            }

        }
        public ErrorOr<AuthenticationResult> GenerateToken(User user,CancellationToken cancellationToken)
        {
            try
            {
                return new AuthenticationResult(GenerateAccessToken(user.Id.ToString(), user.Username, user.Email, user.Role.RoleName), GenerateRefreshToken());
            }
            catch (Exception )
            {
                return General.TokenGeneratorFailure;
            }

        }

    }
}
