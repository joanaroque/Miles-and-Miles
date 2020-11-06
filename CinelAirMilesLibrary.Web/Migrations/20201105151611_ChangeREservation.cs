using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class ChangeREservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Partners_PartnerNameId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "PartnerNameId",
                table: "Reservations",
                newName: "MyPremiumId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_PartnerNameId",
                table: "Reservations",
                newName: "IX_Reservations_MyPremiumId");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationID",
                table: "Reservations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_PremiumOffers_MyPremiumId",
                table: "Reservations",
                column: "MyPremiumId",
                principalTable: "PremiumOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_PremiumOffers_MyPremiumId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReservationID",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "MyPremiumId",
                table: "Reservations",
                newName: "PartnerNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_MyPremiumId",
                table: "Reservations",
                newName: "IX_Reservations_PartnerNameId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Partners_PartnerNameId",
                table: "Reservations",
                column: "PartnerNameId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
