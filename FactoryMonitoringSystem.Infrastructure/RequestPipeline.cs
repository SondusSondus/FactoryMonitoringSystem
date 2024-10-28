
using FactoryMonitoringSystem.Infrastructure.BackgroundJobs;
using FactoryMonitoringSystem.Infrastructure.Cache;
using FactoryMonitoringSystem.Infrastructure.Cors;
using FactoryMonitoringSystem.Infrastructure.HealthCheck;
using FactoryMonitoringSystem.Infrastructure.Logger;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using Microsoft.AspNetCore.Builder;


namespace FactoryMonitoringSystem.Infrastructure
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCorsPolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseBackgroundJobs();
            app.UseLogger();
            app.UseNotifications();
            app.UseHealthCheck();
            app.UseCaching();
            return app;
        }

    }
}
