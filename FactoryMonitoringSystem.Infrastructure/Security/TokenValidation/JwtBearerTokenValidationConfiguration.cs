using FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FactoryMonitoringSystem.Infrastructure.Security.TokenValidation
{

    public sealed class JwtBearerTokenValidationConfiguration(IOptions<JwtSettings> jwtSettings)
        : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public void Configure(string? name, JwtBearerOptions options) => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey))
            };

            // Custom event handler to extract token from HTTP-Only cookie
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    // Try to get the token from the "AccessToken" cookie
                    var accessToken = context.Request.Cookies["AccessToken"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        // Add a custom header to indicate the token has expired
                        context.Response.Headers.Add("Token-Expired", "true");
                        context.Response.StatusCode = 401;  // Unauthorized
                    }
                    return Task.CompletedTask;
                }
            };
        }
    }

}
