using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class MySqlMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReadLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Details = table.Column<string>(nullable: true),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeatherStation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    ExternalKey = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    WeatherStationId = table.Column<int>(nullable: true),
                    Temperature = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureMeasurement_WeatherStation_WeatherStationId",
                        column: x => x.WeatherStationId,
                        principalTable: "WeatherStation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureMeasurement_WeatherStationId",
                table: "TemperatureMeasurement",
                column: "WeatherStationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReadLog");

            migrationBuilder.DropTable(
                name: "TemperatureMeasurement");

            migrationBuilder.DropTable(
                name: "WeatherStation");
        }
    }
}
