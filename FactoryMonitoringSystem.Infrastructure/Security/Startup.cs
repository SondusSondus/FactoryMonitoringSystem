using FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider;
using FactoryMonitoringSystem.Infrastructure.Security.TokenGenerator;
using FactoryMonitoringSystem.Infrastructure.Security.TokenValidation;
using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace FactoryMonitoringSystem.Infrastructure.Security
{
    public static class Startup
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(configuration);
            services.AddAuthorization();
            return services;
        }
        private static IServiceCollection AddAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("User", "Admin"));// This allows you to define multiple roles that can access specific parts of your application.


            });
            services.AddHttpContextAccessor();  // Required to access HttpContext
            services.AddScoped<CurrentUser>(provider => provider.GetRequiredService<ICurrentUserProvider>().GetCurrentUser()); 

            return services;
        }

        private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // Load JWT settings
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));


            // Configure JWT authentication
            services.ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            return services;
        }
    }

}
