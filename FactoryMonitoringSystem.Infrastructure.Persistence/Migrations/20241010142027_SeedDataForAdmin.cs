using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Status", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("299a20c5-9d80-4b39-9b6c-98cbcfffe9e1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 10, 14, 20, 27, 318, DateTimeKind.Utc).AddTicks(9701), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 0, 1, null, null, "Admin" });
        }

        /// <inher Update-Database -Context WriteDbContextitdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("299a20c5-9d80-4b39-9b6c-98cbcfffe9e1"));
        }
    }
}
