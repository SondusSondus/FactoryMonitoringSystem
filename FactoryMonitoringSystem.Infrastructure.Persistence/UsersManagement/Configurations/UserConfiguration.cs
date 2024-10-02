using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FactoryMonitoringSystem.Infrastructure.Persistence.UsersManagement.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table mapping
            builder.ToTable("Users");
            // Primary key
            builder.HasKey(f => f.Id);
            // Property configurations
            builder.Property(f => f.Email)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
