using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedPartnerToAdvertising : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Advertisings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisings_PartnerId",
                table: "Advertisings",
                column: "PartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisings_Partners_PartnerId",
                table: "Advertisings",
                column: "PartnerId",
                principalTable: "Partners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisings_Partners_PartnerId",
                table: "Advertisings");

            migrationBuilder.DropIndex(
                name: "IX_Advertisings_PartnerId",
                table: "Advertisings");

            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Advertisings");
        }
    }
}
