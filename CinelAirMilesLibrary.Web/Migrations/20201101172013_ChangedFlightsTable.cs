using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class ChangedFlightsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departure",
                table: "Flights",
                newName: "Origin");

            migrationBuilder.RenameColumn(
                name: "Arrival",
                table: "Flights",
                newName: "Destination");

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureDate",
                table: "Flights",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureDate",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Flights",
                newName: "Departure");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Flights",
                newName: "Arrival");
        }
    }
}
