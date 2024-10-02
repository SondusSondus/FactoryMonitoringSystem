using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FactoryMonitoringSystem.Infrastructure.ThirdParty
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddThirdParty(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
      

    }
}
