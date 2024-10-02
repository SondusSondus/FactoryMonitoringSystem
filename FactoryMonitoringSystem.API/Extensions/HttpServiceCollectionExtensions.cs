using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FactoryMonitoringSystem.Api.Extensions
{
    public static class HttpServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a default implementation for the <see cref="IHttpContextAccessor"/> service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}