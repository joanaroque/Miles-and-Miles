using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class UpdatedFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Miles",
                table: "Flights");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Miles",
                table: "Flights",
                nullable: false,
                defaultValue: 0);
        }
    }
}
