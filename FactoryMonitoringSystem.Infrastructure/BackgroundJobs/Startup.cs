using Autofac;
using Autofac.Extensions.DependencyInjection;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Hangfire;
using Hangfire.Console;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;

namespace FactoryMonitoringSystem.Infrastructure.BackgroundJobs
{
    public static class Startup
    {
        private static readonly ILogger Logger = Log.ForContext(typeof(Startup));

        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, ConfigureHostBuilder hostBuilder, IConfiguration config)
        {
            services.AddHangfireServer(options => config.GetSection(HangfireStorageSettings.SectionServer).Bind(options));




            var storageSettings = config.GetSection(HangfireStorageSettings.SectionStorage).Get<HangfireStorageSettings>();

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
               // .UseFilter(new LogEverythingAttributeHangfire())
                .UseFilter(new AutomaticRetryAttribute
                {
                    Attempts = 3,  // Reduce retries to 3
                    OnAttemptsExceeded = AttemptsExceededAction.Fail,  // Stop after 3 failures
                    LogEvents = true  // Log retry attempts
                })
            );

         
            return services;
        }
        public static IApplicationBuilder UseBackgroundJobs(this IApplicationBuilder app)
        {
            // Call the scheduler to set up recurring jobs
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var scheduler = scope.ServiceProvider.GetRequiredService<IMonitoringTaskScheduler>();
                scheduler.ScheduleMonitoringTasks();
            }
            GlobalConfiguration.Configuration.UseActivator(new AutofacJobActivator(app.ApplicationServices));

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangFireAuthorizationFilter() },  // Add custom authorization
            });

            return app;
        }
    }
}
