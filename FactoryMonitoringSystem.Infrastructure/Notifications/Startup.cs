using FactoryMonitoringSystem.Infrastructure.Security.CurrentUserProvider;
using FactoryMonitoringSystem.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FactoryMonitoringSystem.Infrastructure.Notifications
{

    public static class Startup
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services,IConfiguration configuration)
        {

            // Bind NotificationSettings section from appsettings.json to NotificationSettings class
            services.Configure<MonitoringSettings>(configuration.GetSection(MonitoringSettings.Section));
            // Add SignalR to the services collection
            services.AddSignalR();

            services.AddScoped(provider => provider.GetRequiredService<ManageUserService>().AssignUserToRoleGroup());

            // Enable response compression
            services.AddResponseCompression(options => options.EnableForHttps = true);
           
        
            return services;
        }
        public static IApplicationBuilder UseNotifications(this IApplicationBuilder app)
        {
          
          
            // Set up SignalR hub endpoints
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapHub<MonitoringHub>("/monitoringHub", options =>
                {
                    options.CloseOnAuthenticationExpiration = true;
                }).RequireAuthorization(); ;
            });
           
            return app;
        }
    }
    
}
