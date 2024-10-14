using FactoryMonitoringSystem.Domain.Factories.Entities;
using FactoryMonitoringSystem.Domain.Machines.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Infrastructure.Persistence.Factories.Configurations;
using FactoryMonitoringSystem.Infrastructure.Persistence.Machines.Configurations;
using FactoryMonitoringSystem.Infrastructure.Persistence.Sensors.Configurations;
using FactoryMonitoringSystem.Infrastructure.Persistence.UsersManagement.Configurations;
using Microsoft.EntityFrameworkCore;


namespace FactoryMonitoringSystem.Infrastructure.Persistence.Common
{
    public class DbContextBase<TContext> : DbContext where TContext : DbContext
    {
        public DbSet<Factory> Factories { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FactoryConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new SensorConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new SensorThresholdConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
