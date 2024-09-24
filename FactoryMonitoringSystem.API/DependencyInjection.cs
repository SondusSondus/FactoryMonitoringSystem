﻿using Autofac.Extensions.DependencyInjection;
using Autofac;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using FactoryMonitoringSystem.Api.Extensions;

namespace FactoryMonitoringSystem.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration,IHostBuilder hostBuilder)
        {
            var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();

            //Register Services to Autofac ContainerBuilder
            // Register Autofac as the service provider factory
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            // Configure Autofac container
            hostBuilder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.RegisterServices(); // Your extension method for registration
            });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
