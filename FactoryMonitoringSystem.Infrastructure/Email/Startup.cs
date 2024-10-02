using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public static class Startup
    {
        public static IServiceCollection AddEmail(this IServiceCollection services,IConfiguration configuration)
        {
            // Bind EmailSettings section from appsettings.json to EmailSettings class
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.Section));
            return services;
        }
    }
}
