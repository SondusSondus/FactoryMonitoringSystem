using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachines.Configurations
{
    public class SensorMachineConfiguration : IEntityTypeConfiguration<SensorMachine>
    {
        public void Configure(EntityTypeBuilder<SensorMachine> builder)
        {
            // Table mapping
            builder.ToTable("SensorMachines");

            // Primary key
            builder.HasKey(s => s.Id);
            builder.HasOne(sm => sm.Sensor)
               .WithMany(s => s.SensorMachines) // Assuming Sensor has a collection of SensorMachines
               .HasForeignKey(sm => sm.SensorId)
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(sm => sm.Machine)
                .WithMany(m => m.SensorMachines) // Assuming Machine has a collection of SensorMachines
                .HasForeignKey(sm => sm.MachineId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
