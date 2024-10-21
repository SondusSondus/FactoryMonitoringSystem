using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Sensors.Configurations
{
    public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
    {
        public void Configure(EntityTypeBuilder<Sensor> builder)
        {
            // Table mapping
            builder.ToTable("Sensors");

            // Primary key
            builder.HasKey(s => s.Id);

            // Property configurations
            builder.Property(s => s.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.MinValue)
                .IsRequired();
            
            builder.Property(s => s.MaxValue)
                .IsRequired();

            builder.Property(s => s.Unit)
                .IsRequired()
                .HasMaxLength(20);

         
        }
    }
}
