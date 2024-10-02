using FactoryMonitoringSystem.Shared.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryMonitoringSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });

            return services;
        }
    }
}
