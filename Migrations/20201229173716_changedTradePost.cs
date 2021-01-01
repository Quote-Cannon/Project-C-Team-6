using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthSystem.Migrations
{
    public partial class changedTradePost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Trade",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Post",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Trade",
                table: "Products",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "Post",
                table: "Products",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
