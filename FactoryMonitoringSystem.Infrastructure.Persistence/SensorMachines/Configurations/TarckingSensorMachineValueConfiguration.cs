using FactoryMonitoringSystem.Domain.SensorMachines.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachines.Configurations
{
    class TarckingSensorMachineValueConfiguration : IEntityTypeConfiguration<TarckingSensorMachineValue>
    {
        public void Configure(EntityTypeBuilder<TarckingSensorMachineValue> builder)
        {
            // Table mapping
            builder.ToTable("TarckingSensorMachineValues");

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