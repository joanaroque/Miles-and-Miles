using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class DeletedUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientComplaints_AspNetUsers_ClientId",
                table: "ClientComplaints");

            migrationBuilder.DropIndex(
                name: "IX_ClientComplaints_ClientId",
                table: "ClientComplaints");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientComplaints");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "ClientComplaints",
                newName: "Body");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Body",
                table: "ClientComplaints",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "ClientComplaints",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientComplaints_ClientId",
                table: "ClientComplaints",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientComplaints_AspNetUsers_ClientId",
                table: "ClientComplaints",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
