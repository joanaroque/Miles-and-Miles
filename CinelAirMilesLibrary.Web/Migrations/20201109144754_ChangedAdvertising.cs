using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class ChangedAdvertising : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Advertisings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_FlightId",
                table: "Advertisings",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_Flights_FlightId",
                table: "Advertisings",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_Flights_FlightId",
                table: "Advertisings");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_FlightId",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Advertisings");
        }
    }
}
