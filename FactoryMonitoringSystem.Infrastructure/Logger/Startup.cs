

using FactoryMonitoringSystem.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace FactoryMonitoringSystem.Infrastructure.Logger
{
    public static class Startup
    {
        public static IServiceCollection AddLogger(this IServiceCollection services , ConfigureHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog(); // Replace default logging with Serilog
            // Configure Serilog for structured logging
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()  // Enrich logs with contextual information
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();
            return services;
        }

        public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging(); // Automatically log HTTP requests// Request logging
            app.UseMiddleware<HealthCheckLoggingMiddleware>(); // Log health check results// Custom middleware
            return app;
        }
    }
}
