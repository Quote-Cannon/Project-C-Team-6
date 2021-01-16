using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthSystem.Migrations
{
    public partial class Reportss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reason",
                table: "Reports",
                newName: "Explanation");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Reports",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Reports");

            migrationBuilder.RenameColumn(
                name: "Explanation",
                table: "Reports",
                newName: "Reason");
        }
    }
}
