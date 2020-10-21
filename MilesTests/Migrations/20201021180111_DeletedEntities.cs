using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class DeletedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatsAvailables");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeatsAvailables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AvailableSeats = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    FlightNumber = table.Column<int>(nullable: false),
                    MaximumSeats = table.Column<int>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatsAvailables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatsAvailables_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeatsAvailables_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatsAvailables_CreatedById",
                table: "SeatsAvailables",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SeatsAvailables_ModifiedById",
                table: "SeatsAvailables",
                column: "ModifiedById");
        }
    }
}
