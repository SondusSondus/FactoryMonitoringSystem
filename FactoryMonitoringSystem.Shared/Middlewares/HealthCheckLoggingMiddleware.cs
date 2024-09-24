using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Shared.Middlewares
{
    public class HealthCheckLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HealthCheckLoggingMiddleware> _logger;
        public HealthCheckLoggingMiddleware(RequestDelegate next,ILogger<HealthCheckLoggingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/health"))
            {
                var healthReport = await context.RequestServices
                    .GetRequiredService<HealthCheckService>()
                    .CheckHealthAsync();

                LogHealthCheckResults(healthReport);
            }

            await _next(context);
        }

        private void LogHealthCheckResults(HealthReport report)
        {
            foreach (var entry in report.Entries)
            {
                var healthStatus = entry.Value.Status;
                var description = entry.Value.Description;
                var data = JsonSerializer.Serialize(entry.Value.Data);

                switch (healthStatus)
                {
                    case HealthStatus.Healthy:
                        _logger.LogInformation("Health check '{HealthCheckName}' is healthy: {Description}. Data: {Data}",
                            entry.Key, description, data);
                        break;
                    case HealthStatus.Degraded:
                        _logger.LogWarning("Health check '{HealthCheckName}' is degraded: {Description}. Data: {Data}",
                            entry.Key, description, data);
                        break;
                    case HealthStatus.Unhealthy:
                        _logger.LogError("Health check '{HealthCheckName}' is unhealthy: {Description}. Data: {Data}",
                            entry.Key, description, data);
                        break;
                }
            }
        }
    }
}
