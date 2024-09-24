using FactoryMonitoringSystem.Domain.Sensors.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.Property(s => s.Value)
                .IsRequired();

            builder.Property(s => s.Unit)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}
