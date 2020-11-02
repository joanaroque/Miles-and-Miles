using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedFlightToOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flight",
                table: "PremiumOffers");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "PremiumOffers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PremiumOffers_FlightId",
                table: "PremiumOffers",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_PremiumOffers_Flights_FlightId",
                table: "PremiumOffers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PremiumOffers_Flights_FlightId",
                table: "PremiumOffers");

            migrationBuilder.DropIndex(
                name: "IX_PremiumOffers_FlightId",
                table: "PremiumOffers");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "PremiumOffers");

            migrationBuilder.AddColumn<string>(
                name: "Flight",
                table: "PremiumOffers",
                nullable: true);
        }
    }
}
