using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Hangfire;
using Hangfire.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{
    public static class Startup
    {
        private static readonly ILogger Logger = Log.ForContext(typeof(Startup));

        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration config)
        {
            services.AddHangfireServer(options => config.GetSection("HangfireSettings:Server").Bind(options));

            //services.AddHangfireConsoleExtensions();

            var storageSettings = config.GetSection("HangfireSettings:Storage").Get<HangfireStorageSettings>();

            if (string.IsNullOrEmpty(storageSettings?.StorageProvider)) throw new Exception("Hangfire Storage Provider is not configured.");
            if (string.IsNullOrEmpty(storageSettings.ConnectionString)) throw new Exception("Hangfire Storage Provider ConnectionString is not configured.");
            Logger.Information($"Hangfire: Current Storage Provider : {storageSettings.StorageProvider}");
            Logger.Information("Hangfire storage");

            var appOptions = config.GetSection(nameof(AppOptions)).Get<AppOptions>();
            var connectionString = string.IsNullOrEmpty(appOptions?.WriteDatabaseConnectionString)
                ? storageSettings.ConnectionString
                : appOptions.WriteDatabaseConnectionString;

            services.AddHangfire((provider, hangfireConfig) => hangfireConfig
                .UseSqlServerStorage(connectionString)
                .UseFilter(new LogEverythingAttributeHangfire())
                .UseFilter(new HangFireAuthorizationFilter())
                .UseFilter(new AutomaticRetryAttribute
                {
                    Attempts = 3,  // Reduce retries to 3
                    OnAttemptsExceeded = AttemptsExceededAction.Fail,  // Stop after 3 failures
                    LogEvents = true  // Log retry attempts
                })
                .UseSerializerSettings(new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })
                .UseConsole()
            );

            return services;
        }
        public static  IApplicationBuilder UseBackgroundJobs(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() },  // Add custom authorization
                AppPath = "/"  // Return to your main app after visiting the dashboard
            });
            return app;
        }
    }
}
