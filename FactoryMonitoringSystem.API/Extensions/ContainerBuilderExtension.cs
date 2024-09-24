
using Autofac;
using FactoryMonitoringSystem.Application;
using FactoryMonitoringSystem.Application.Common.Services;
using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Domain;
using FactoryMonitoringSystem.Infrastructure.Persistence;
using FactoryMonitoringSystem.Shared;
using System;
using System.Reflection;

namespace FactoryMonitoringSystem.Api.Extensions
{
    public static class ContainerBuilderExtension
    {
        // Method to retrieve assemblies based on an array of marker types
        public static void RegisterServices(this ContainerBuilder containerBuilder)
        {
            //containerBuilder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //containerBuilder.RegisterGeneric(typeof(EfReadOnlyRepository<>)).As(typeof(IReadOnlyRepository<>)).InstancePerLifetimeScope();

            var applicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
            var dominAssembly = typeof(IDomainAssemblyMarker).Assembly;
            var persistenceAssembly = typeof(IPersistenceAssemblyMarker).Assembly;
            var SharedAssembly = typeof(ISharedAssemblyMarker).Assembly;
           

            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, SharedAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, SharedAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, SharedAssembly)
                .AssignableTo<ISingletonDependency>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // Register Mapster's IMapper
            containerBuilder.RegisterType<MapsterMapper.Mapper>()
                .As<MapsterMapper.IMapper>()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterType<EmailNotificationStrategy>().Named<INotificationStrategy>(nameof(EmailNotificationStrategy));
            containerBuilder.RegisterType<InAppNotificationStrategy>().Named<INotificationStrategy>(nameof(InAppNotificationStrategy));

        }


    }
}
