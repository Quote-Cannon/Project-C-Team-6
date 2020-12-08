using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantWebsite.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "Users",
                newName: "UserDate");

            migrationBuilder.RenameColumn(
                name: "WrittenReview",
                table: "Review",
                newName: "ReviewDescription");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewDate",
                table: "Review",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReviewTitle",
                table: "Review",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewDate",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewTitle",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "UserDate",
                table: "Users",
                newName: "RegisterDate");

            migrationBuilder.RenameColumn(
                name: "ReviewDescription",
                table: "Review",
                newName: "WrittenReview");
        }
    }
}
