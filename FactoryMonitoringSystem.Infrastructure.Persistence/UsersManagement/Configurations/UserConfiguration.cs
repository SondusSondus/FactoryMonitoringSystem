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
            builder.HasKey(user => user.Id);
            // Property configurations
            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(user => user.Role)
                .WithOne()
                .HasForeignKey<User>(user=>user.RoleId)
                .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
