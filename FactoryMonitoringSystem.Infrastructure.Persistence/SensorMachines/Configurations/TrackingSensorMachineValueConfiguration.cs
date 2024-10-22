using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachines.Configurations
{
    class TrackingSensorMachineValueConfiguration : IEntityTypeConfiguration<TrackingSensorMachineValue>
    {
        public void Configure(EntityTypeBuilder<TrackingSensorMachineValue> builder)
        {
            // Table mapping
            builder.ToTable("TrackingSensorMachineValues");

            builder.HasIndex(prop => prop.Id);
            builder.Property(prop => prop.Value)
                    .IsRequired(); 
            builder.Property(prop => prop.SensorMachineId)
                    .IsRequired();
            builder.Property(prop => prop.DateOfReadingValue)
                    .IsRequired();


        }

    }
}