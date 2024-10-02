using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMonitoringSystem.Infrastructure.Persistence.UsersManagement.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UsersRole");
            builder.HasKey(userRole => new { userRole.RoleId , userRole.UserId });
            builder.HasOne(userRole => userRole.Role).WithMany().HasForeignKey(userRole => userRole.RoleId);
            builder.HasOne(userRole => userRole.User).WithMany().HasForeignKey(userRole => userRole.UserId);
        }
    }
}
