using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedToFlight2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Miles",
                table: "Flights",
                newName: "Distance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Distance",
                table: "Flights",
                newName: "Miles");
        }
    }
}
