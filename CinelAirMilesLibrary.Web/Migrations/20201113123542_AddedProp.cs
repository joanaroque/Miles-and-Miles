using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Advertisings");

            migrationBuilder.AddColumn<byte>(
                name: "Image",
                table: "PremiumOffers",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Image",
                table: "Advertisings",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "PremiumOffers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Advertisings");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Advertisings",
                nullable: true);
        }
    }
}
