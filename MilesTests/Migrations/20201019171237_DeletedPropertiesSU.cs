using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class DeletedPropertiesSU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "TierChanges");

            migrationBuilder.DropColumn(
                name: "ConfirmSeatsAvailable",
                table: "SeatsAvailables");

            migrationBuilder.DropColumn(
                name: "PendingComplaint",
                table: "ClientComplaints");

            migrationBuilder.DropColumn(
                name: "PendingPublish",
                table: "Advertisings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "TierChanges",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmSeatsAvailable",
                table: "SeatsAvailables",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PendingComplaint",
                table: "ClientComplaints",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PendingPublish",
                table: "Advertisings",
                nullable: false,
                defaultValue: false);
        }
    }
}
