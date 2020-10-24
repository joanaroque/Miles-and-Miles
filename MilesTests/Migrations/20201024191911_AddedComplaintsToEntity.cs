using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedComplaintsToEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ClientComplaints");

            migrationBuilder.AddColumn<int>(
                name: "Complaint",
                table: "ClientComplaints",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complaint",
                table: "ClientComplaints");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ClientComplaints",
                nullable: false,
                defaultValue: "");
        }
    }
}
