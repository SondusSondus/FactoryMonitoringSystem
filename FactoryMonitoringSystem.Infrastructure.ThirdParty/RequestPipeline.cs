
using Microsoft.AspNetCore.Builder;


namespace FactoryMonitoringSystem.Infrastructure.ThirdParty
{
    public static class RequestPipeline
    { 
        public static IApplicationBuilder UseThirdParty(this IApplicationBuilder app)
        {
            return app;
        }
        
    }
}
