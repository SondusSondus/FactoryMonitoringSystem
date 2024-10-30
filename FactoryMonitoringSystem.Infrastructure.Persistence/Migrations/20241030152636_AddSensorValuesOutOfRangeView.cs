using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FactoryMonitoringSystem.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorValuesOutOfRangeView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE VIEW View_SensorValuesOutOfRange AS
            SELECT 
                 TSM.Value AS Value,
                 S.Name AS SensorName,
                 M.Name AS MachineName
             FROM 
                 TrackingSensorMachineValues AS TSM
             LEFT JOIN 
                 SensorMachines AS SM ON SM.Id = TSM.SensorMachineId
             LEFT JOIN 
                 Sensors AS S ON S.Id = SM.SensorId
             LEFT JOIN 
                 Machines AS M ON M.Id = SM.MachineId
             WHERE 
                 (TSM.Value < S.MinValue OR TSM.Value > S.MaxValue)
                 AND TSM.IsThresholdBreached = 0;
        ");
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
