using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
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
            builder.Property(role => role.Id)
                   .ValueGeneratedNever();
            builder.Property(role => role.RoleName).IsRequired();
            builder.HasData(
                            new Role { Id = (int)RolesEnum.Admin, RoleName = RolesEnum.Admin.ToString() },
                            new Role { Id = (int)RolesEnum.User, RoleName = RolesEnum.User.ToString() 
                            });
        }
    }
}
