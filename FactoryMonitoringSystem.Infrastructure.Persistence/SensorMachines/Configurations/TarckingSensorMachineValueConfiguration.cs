using FactoryMonitoringSystem.Domain.SensorMachine.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.SensorMachine.Configurations
{
    class TarckingSensorMachineValueConfiguration : IEntityTypeConfiguration<TarckingSensorMachineValue>
    {
        public void Configure(EntityTypeBuilder<TarckingSensorMachineValue> builder)
        {
            // Table mapping
            builder.ToTable("SensorThresholds");

            // Primary key




        }
    }
}