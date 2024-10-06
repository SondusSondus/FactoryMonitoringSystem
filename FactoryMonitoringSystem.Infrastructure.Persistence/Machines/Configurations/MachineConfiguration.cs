using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using FactoryMonitoringSystem.Domain.Machines.Entities;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Machines.Configurations
{
    public class MachineConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            // Table mapping
            builder.ToTable("Machines");

            // Primary key
            builder.HasKey(m => m.Id);

            // Property configurations
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Type)
                .IsRequired()
                .HasMaxLength(50);

            // Relationship configuration
            builder.HasMany(m => m.Sensors)
                .WithOne().HasForeignKey(s=>s.MachineId)
                .OnDelete(DeleteBehavior.Cascade); // Define delete behavior
        }
    }
}
