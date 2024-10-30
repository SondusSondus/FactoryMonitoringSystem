using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColToTrackingSensorMachineValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TarckingSensorMachineValues");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bfe2a07e-9701-44ed-a64e-7719d7db3cf0"));

            migrationBuilder.CreateTable(
                name: "TrackingSensorMachineValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SensorMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    DateOfReadingValue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsThresholdBreached = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingSensorMachineValues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("9a4eb747-23de-4349-b393-cae8f0d507f9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 30, 14, 59, 59, 336, DateTimeKind.Utc).AddTicks(4656), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 1, null, null, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingSensorMachineValues_Id",
                table: "TrackingSensorMachineValues",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingSensorMachineValues");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9a4eb747-23de-4349-b393-cae8f0d507f9"));

            migrationBuilder.CreateTable(
                name: "TarckingSensorMachineValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfReadingValue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SensorMachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarckingSensorMachineValues", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "EmailVerificationCode", "EmailVerificationCodeExpiration", "FailedLoginAttempts", "IsEmailVerified", "LockoutEnd", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "RoleId", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[] { new Guid("bfe2a07e-9701-44ed-a64e-7719d7db3cf0"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2024, 10, 20, 19, 25, 26, 807, DateTimeKind.Utc).AddTicks(3346), null, null, "s.ondus.samara94@gmail.com", null, null, 0, true, null, "$2a$11$7Phvr48TV1QchCJiuCjnmuDLqRVDQ6TmJmJaMmnexJfc/xIp6u.yO", null, null, 1, null, null, "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_TarckingSensorMachineValues_Id",
                table: "TarckingSensorMachineValues",
                column: "Id");
        }
    }
}
