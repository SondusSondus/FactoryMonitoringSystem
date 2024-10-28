using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryMonitoringSystem.Infrastructure.Cache
{
    public static class Startup
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            // In-memory caching
            services.AddMemoryCache();
            return services;

        }

        public static IApplicationBuilder UseCaching(this IApplicationBuilder app)
        {
            return app;
        }
    }

}
