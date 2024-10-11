using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNameColToSenser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9a70d6c-267b-4a2e-a0c5-a49cddf7c22c"));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sensors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Status", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("59e94d2f-886f-40af-a6ce-75d575a01543"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 11, 21, 23, 53, 558, DateTimeKind.Utc).AddTicks(1210), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 0, 1, null, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("59e94d2f-886f-40af-a6ce-75d575a01543"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sensors");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Status", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("e9a70d6c-267b-4a2e-a0c5-a49cddf7c22c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 11, 20, 14, 9, 429, DateTimeKind.Utc).AddTicks(847), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 0, 1, null, null, "Admin" });
        }
    }
}
