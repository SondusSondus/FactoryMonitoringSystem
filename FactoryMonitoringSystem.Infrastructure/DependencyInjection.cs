
using FactoryMonitoringSystem.Infrastructure.BackgroundJobs;
using FactoryMonitoringSystem.Infrastructure.Cors;
using FactoryMonitoringSystem.Infrastructure.Email;
using FactoryMonitoringSystem.Infrastructure.HealthCheck;
using FactoryMonitoringSystem.Infrastructure.Logger;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using FactoryMonitoringSystem.Infrastructure.Persistence;
using FactoryMonitoringSystem.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FactoryMonitoringSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ConfigureHostBuilder hostBuilder)
        {

            services.AddCorsPolicy(configuration)
                    .AddSecurity(configuration)
                    .AddBackgroundJobs(hostBuilder,configuration)
                    .AddPersistence(configuration)
                    .AddEmail(configuration)
                    .AddNotifications(configuration)
                    .AddILogger(hostBuilder)
                    .AddIHealthCheck(configuration);
            return services;
        }
    }
}
