using FactoryMonitoringSystem.Infrastructure.Persistence.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace FactoryMonitoringSystem.Infrastructure.Security
{
    public static class Startup
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddAuthentication();
            services.AddAuthorization();
            return services;
        }
        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });
            //services.AddScoped<IAuthorizationService, AuthorizationService>();
            //services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
            //services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

            //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            //services
            //    .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            //    .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer();

            return services;
        }
    }

}
