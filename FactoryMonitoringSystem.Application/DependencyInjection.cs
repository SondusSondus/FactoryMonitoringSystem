using Autofac;
using FactoryMonitoringSystem.Application.Common.Services;
using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Application.Contracts.Common.Mappings;
using FactoryMonitoringSystem.Shared.Behaviors;
using FactoryMonitoringSystem.Shared.Utilities.General;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FactoryMonitoringSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
                options.AddOpenBehavior(typeof(ValidationBehavior<,>));

            });

            return services;
        }
    }
}
