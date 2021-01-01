using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthSystem.Migrations
{
    public partial class changedPostToDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Post",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Delivery",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
