﻿using FactoryMonitoringSystem.Domain.UsersManagement.Entities;
using FactoryMonitoringSystem.Shared.Utilities.Enums;
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

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasOne(user => user.Role)
                .WithMany().HasForeignKey(user => user.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(
                           new User
                           {
                               Id = Guid.NewGuid(),
                               Username = "Admin",
                               Email = "youremail",
                               RoleId = (int)RolesEnum.Admin,
                               IsEmailVerified = true,
                               PasswordHash = "$2a$11$tY9GFaoDWF8iw.NyaHu3x.e9iBaeUWjGb9pW7N5DFtmKDO6HmTB3C" 

                           });
           

        }
    }
}
