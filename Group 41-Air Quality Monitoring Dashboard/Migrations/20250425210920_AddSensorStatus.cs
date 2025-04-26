using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Group_41_Air_Quality_Monitoring_Dashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Sensors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Sensors");
        }
    }
}
