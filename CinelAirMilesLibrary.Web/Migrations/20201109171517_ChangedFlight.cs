using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class ChangedFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Origin",
                table: "Flights",
                newName: "Departure");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Flights",
                newName: "Arrival");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departure",
                table: "Flights",
                newName: "Origin");

            migrationBuilder.RenameColumn(
                name: "Arrival",
                table: "Flights",
                newName: "Destination");
        }
    }
}
