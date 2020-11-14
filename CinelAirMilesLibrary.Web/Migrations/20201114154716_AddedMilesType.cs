using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MilesBackOffice.Web.Migrations
{
    public partial class AddedMilesType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "MilesType",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionType",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccountApprovedDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MilesType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountApprovedDate",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);
        }
    }
}
