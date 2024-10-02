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
        private string GenerateAccessToken(string id, string userName, string email, IReadOnlyList<string> roles)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, userName),
                new(JwtRegisteredClaimNames.Email, email ),
                new("id",  id),
                new("expiryTime", DateTime.UtcNow.ToString()),
            };

            roles.ToList().ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
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
                        _currentUserProvider.GetCurrentUser().Email, _currentUserProvider.GetCurrentUser().Roles), GenerateRefreshToken());
            }
            catch (Exception ex)
            {
                return General.TokenGeneratorFailure;
            }

        }
        public ErrorOr<AuthenticationResult> GenerateToken(User user,CancellationToken cancellationToken)
        {
            try
            {
                return new AuthenticationResult(GenerateAccessToken(user.Id.ToString(), user.Username, user.Email,
                user.UserRoles.Select(userRole => userRole.Role.RoleName).ToList().AsReadOnly()), GenerateRefreshToken());
            }
            catch (Exception ex)
            {
                return General.TokenGeneratorFailure;
            }

        }

    }
}
