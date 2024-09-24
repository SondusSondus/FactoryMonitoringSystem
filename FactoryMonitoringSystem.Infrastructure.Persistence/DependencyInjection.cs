using FactoryMonitoringSystem.Domain.Common.Repositories;
using FactoryMonitoringSystem.Domain.Common.Specifications;
using FactoryMonitoringSystem.Infrastructure.Persistence.Common;
using FactoryMonitoringSystem.Shared.Utilities.GeneralModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace FactoryMonitoringSystem.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var appOptions = configuration.GetSection(nameof(AppOptions)).Get<AppOptions>();

            services.AddScoped(typeof(IBaseSpecification<>),typeof(BaseSpecification<>));
            services.AddScoped(typeof(IWriteRepository<>),typeof(WriteRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            // Configure Write DbContext
            services.AddDbContext<WriteDbContext>(options =>
                options.UseSqlServer(appOptions.WriteDatabaseConnectionString, sqlOptions =>
                {

                    sqlOptions.CommandTimeout(30);  // Set the command timeout to 30 seconds
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);  // Configures SQL Server's transient fault handling
                }));
            // Configure Read DbContext
            services.AddDbContext<ReadDbContext>(options =>
                options.UseSqlServer(appOptions.ReadDatabaseConnectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);  // Configures SQL Server's transient fault handling
                }));
            

            return services;
        }

    }
}
