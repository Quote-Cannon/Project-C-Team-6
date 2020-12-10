using Microsoft.EntityFrameworkCore.Migrations;

namespace PlantWebsite.Migrations
{
    public partial class addedPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Post",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Post",
                table: "Products");
        }
    }
}
