
using FactoryMonitoringSystem.Infrastructure.BackgroundJobs;
using FactoryMonitoringSystem.Infrastructure.Cors;
using FactoryMonitoringSystem.Infrastructure.HealthCheck;
using FactoryMonitoringSystem.Infrastructure.Logger;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using FactoryMonitoringSystem.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;
using Serilog;


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
            return app;
        }
        
    }
}
