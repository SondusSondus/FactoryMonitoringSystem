﻿
using FactoryMonitoringSystem.Infrastructure.BackgroundJobs;
using FactoryMonitoringSystem.Infrastructure.Cors;
using FactoryMonitoringSystem.Infrastructure.Email;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FactoryMonitoringSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace FactoryMonitoringSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddCorsPolicy(configuration)
                    .AddBackgroundJobs(configuration)
                    .AddPersistence(configuration)
                    .AddEmail(configuration)
                    .AddNotifications(configuration)
                    .AddLogging()
                    .AddHealthChecks();
            return services;
        }
    }
}