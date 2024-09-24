using FactoryMonitoringSystem.Shared.Utilities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Email
{
    public static class Startup
    {
        public static IServiceCollection AddEmail(this IServiceCollection services,IConfiguration configuration)
        {
            // Bind EmailSettings section from appsettings.json to EmailSettings class
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            return services;
        }
    }
}
