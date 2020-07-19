using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class MissionIdeaDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EndDate",
                table: "MissionIdeas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "MissionIdeas",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StartDate",
                table: "MissionIdeas",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "MissionIdeas");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "MissionIdeas");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "MissionIdeas");
        }
    }
}
