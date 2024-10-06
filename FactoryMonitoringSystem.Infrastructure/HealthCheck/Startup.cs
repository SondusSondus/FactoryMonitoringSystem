using FactoryMonitoringSystem.Shared.Middlewares;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;


namespace FactoryMonitoringSystem.Infrastructure.HealthCheck
{
    public static class Startup
    {

        public static IServiceCollection AddIHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {

            var appOptions = configuration.GetSection(AppOptions.Section).Get<AppOptions>();
            services.AddHealthChecks()
           .AddSqlServer(
            appOptions.WriteDatabaseConnectionString,
            healthQuery: "SELECT 1;",  // Optional: You can specify a custom query to check database health
            name: "SQL Server",       // Name of the health check
            failureStatus: HealthStatus.Unhealthy,  // Specify the status when the health check fails
            tags: new[] { "db", "sql" });           // Optional: Tags to categorize health checks
            return services;
        }
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder app)
        {
            app.UseMiddleware<HealthCheckLoggingMiddleware>(); // Log health check results// Custom middleware

            // Ensure the health check endpoint is correctly mapped
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse, // Provides detailed JSON response
                    Predicate = _ => true // Include all health checks
                });
            });
                
            return app;
        }
    }
}
