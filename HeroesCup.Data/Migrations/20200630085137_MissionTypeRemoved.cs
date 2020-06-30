using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class MissionTypeRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeheroesUrl",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Missions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeheroesUrl",
                table: "Missions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
