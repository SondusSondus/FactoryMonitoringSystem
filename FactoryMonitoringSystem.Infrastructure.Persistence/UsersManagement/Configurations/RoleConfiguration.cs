using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FactoryMonitoringSystem.Infrastructure.Persistence.UsersManagement.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(role => role.Id);
            builder.Property(role=>role.RoleName).IsRequired();


        }
    }
}
