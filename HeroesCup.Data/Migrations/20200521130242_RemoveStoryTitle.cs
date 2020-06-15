using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class RemoveStoryTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Stories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Stories",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}