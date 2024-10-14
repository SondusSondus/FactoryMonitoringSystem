using FactoryMonitoringSystem.Domain.SensorMachine.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachine.Configurations
{
    public class SensorMachineConfiguration : IEntityTypeConfiguration<SensorMachine>
    {
        public void Configure(EntityTypeBuilder<SensorMachine> builder)
        {
            // Table mapping
            builder.ToTable("SensorMachines");

            // Primary key
            builder.HasKey(s => s.Id);

            builder.Property(s => s.SensorId)
                .IsRequired();
            builder.Property(s => s.machineId)
                    .IsRequired();

            builder.HasOne(s => s.Sensor)
                    .WithMany().HasForeignKey(s => s.SensorId)
                    .OnDelete(DeleteBehavior.Cascade); // Define delete behavior
            builder.HasOne(s => s.Sensor)
                     .WithMany().HasForeignKey(s => s.SensorId)
                     .OnDelete(DeleteBehavior.Cascade); // Define delete behavior
        }
    }
}
