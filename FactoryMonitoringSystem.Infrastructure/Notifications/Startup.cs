using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Notifications
{

    public static class Startup
    {
        public static IServiceCollection AddNotifications(this IServiceCollection services,IConfiguration configuration)
        {

            // Bind NotificationSettings section from appsettings.json to NotificationSettings class
            services.Configure<NotificationSettings>(configuration.GetSection("NotificationSettings"));
            // Add SignalR to the services collection
            services.AddSignalR();

            return services;
        }
        public static IApplicationBuilder UseNotifications(this IApplicationBuilder app)
        {            
            // Set up SignalR hub endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationHub", options =>
                {
                    options.CloseOnAuthenticationExpiration = true;
                }).RequireAuthorization(); ;
            });
           
            return app;
        }
    }
    
}
