using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class RemovedIsConfirmedIEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "TypePremiuns");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "PremiumOffers");

            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Partners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "TypePremiuns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "PremiumOffers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Partners",
                nullable: false,
                defaultValue: false);
        }
    }
}
