
using Autofac;
using FactoryMonitoringSystem.Application;
using FactoryMonitoringSystem.Application.Common.Services;
using FactoryMonitoringSystem.Application.Contracts.Common.CommonEvent;
using FactoryMonitoringSystem.Domain;
using FactoryMonitoringSystem.Infrastructure;
using FactoryMonitoringSystem.Infrastructure.Notifications;
using FactoryMonitoringSystem.Infrastructure.Persistence;
using FactoryMonitoringSystem.Shared;

namespace FactoryMonitoringSystem.Api.Extensions
{
    public static class ContainerBuilderExtension
    {
        // Method to retrieve assemblies based on an array of marker types
        public static void RegisterServices(this ContainerBuilder containerBuilder)
        {

            // Register dependencies for MonitoringTaskScheduler
            containerBuilder.RegisterType<MachineMonitoringService>()
                            .AsSelf()
                            .InstancePerDependency();
            containerBuilder.RegisterType<MonitoringTaskScheduler>()
                            .As<IMonitoringTaskScheduler>()
                            .SingleInstance();

            var applicationAssembly = typeof(IApplicationAssemblyMarker).Assembly;
            var dominAssembly = typeof(IDomainAssemblyMarker).Assembly;
            var persistenceAssembly = typeof(IPersistenceAssemblyMarker).Assembly;
            var sharedAssembly = typeof(ISharedAssemblyMarker).Assembly;
            var infrastructureAssembly = typeof(IInfrastructureAssemblyMarker).Assembly;


            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, sharedAssembly, infrastructureAssembly)
                .AssignableTo<IScopedDependency>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, sharedAssembly, infrastructureAssembly)
                .AssignableTo<ITransientDependency>()
                .AsImplementedInterfaces()
                .InstancePerDependency();

            containerBuilder.RegisterAssemblyTypes(applicationAssembly, dominAssembly, persistenceAssembly, sharedAssembly, infrastructureAssembly)
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
