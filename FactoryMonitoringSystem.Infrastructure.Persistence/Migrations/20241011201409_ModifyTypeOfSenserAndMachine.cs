using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTypeOfSenserAndMachine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("299a20c5-9d80-4b39-9b6c-98cbcfffe9e1"));

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Sensors",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Machines",
                type: "int",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Status", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("e9a70d6c-267b-4a2e-a0c5-a49cddf7c22c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 11, 20, 14, 9, 429, DateTimeKind.Utc).AddTicks(847), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 0, 1, null, null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e9a70d6c-267b-4a2e-a0c5-a49cddf7c22c"));

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Sensors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Machines",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 50);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "Status", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("299a20c5-9d80-4b39-9b6c-98cbcfffe9e1"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 10, 14, 20, 27, 318, DateTimeKind.Utc).AddTicks(9701), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 0, 1, null, null, "Admin" });
        }
    }
}
