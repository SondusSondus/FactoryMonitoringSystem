using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("da977deb-2ba2-494f-9893-9a8e80594c66"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("9052daa7-8843-4b5a-8d85-a299d0af0ffd"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 11, 9, 21, 19, 32, 458, DateTimeKind.Utc).AddTicks(6606), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 1, null, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9052daa7-8843-4b5a-8d85-a299d0af0ffd"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("da977deb-2ba2-494f-9893-9a8e80594c66"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 30, 15, 26, 36, 75, DateTimeKind.Utc).AddTicks(4425), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 1, null, null, "Admin" });
        }
    }
}
