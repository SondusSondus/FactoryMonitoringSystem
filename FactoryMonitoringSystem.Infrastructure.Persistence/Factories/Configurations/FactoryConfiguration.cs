using FactoryMonitoringSystem.Domain.Factories.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Factories.Configurations
{
    public class FactoryConfiguration : IEntityTypeConfiguration<Factory>
    {
        public void Configure(EntityTypeBuilder<Factory> builder)
        {
            // Table mapping
            builder.ToTable("Factories");

            // Primary key
            builder.HasKey(f => f.Id);

            // Property configurations
            builder.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.Location)
                .IsRequired()
                .HasMaxLength(200);

            // Relationship configuration
            builder.HasMany(f => f.Machines)
                .WithOne()
                .HasForeignKey(m=>m.FactoryId)
                .OnDelete(DeleteBehavior.Cascade); // Define delete behavior
        }
    }
}
