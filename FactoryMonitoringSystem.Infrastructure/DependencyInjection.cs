
using FactoryMonitoringSystem.Infrastructure.BackgroundJobs;
using FactoryMonitoringSystem.Infrastructure.Cors;
using FactoryMonitoringSystem.Infrastructure.Email;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FactoryMonitoringSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FactoryMonitoringSystem.Infrastructure.HealthCheck;
using FactoryMonitoringSystem.Infrastructure.Logger;
using Microsoft.AspNetCore.Builder;
using FactoryMonitoringSystem.Infrastructure.Security;
namespace FactoryMonitoringSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration, ConfigureHostBuilder hostBuilder)
        {
     
            services.AddCorsPolicy(configuration)
                    .AddSecurity(configuration)
                    .AddBackgroundJobs(configuration)
                    .AddPersistence(configuration)
                    .AddEmail(configuration)
                    .AddNotifications(configuration)
                    .AddILogger(hostBuilder)
                    .AddIHealthCheck(configuration);
            return services;
        }
    }
}
