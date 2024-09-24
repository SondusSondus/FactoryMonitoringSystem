using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace FactoryMonitoringSystem.Infrastructure.Cors
{
    public static class Startup
    {
        private const string CorsPolicy = nameof(CorsPolicy);
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
        {
            var corsSettings = config.GetSection(nameof(CorsSettings)).Get<CorsSettings>();
            var origins = new List<string>();
            if (corsSettings?.AppAllowedCorsOrigins is not null)
                origins.AddRange(corsSettings.AppAllowedCorsOrigins);
            if (corsSettings?.ExternalAppAllowedCorsOrigins is not null)
                origins.AddRange(corsSettings.ExternalAppAllowedCorsOrigins);

            return services.AddCors(opt =>
                opt.AddPolicy(CorsPolicy, policy =>
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(origins.ToArray())
                        .WithExposedHeaders("Content-Disposition")));
        }

        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app) =>
            app.UseCors(CorsPolicy);
    }
}
