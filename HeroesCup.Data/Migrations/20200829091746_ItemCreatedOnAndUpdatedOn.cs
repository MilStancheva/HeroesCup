using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class ItemCreatedOnAndUpdatedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedOn",
                table: "Stories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedOn",
                table: "Stories",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedOn",
                table: "Missions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedOn",
                table: "Missions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedOn",
                table: "MissionIdeas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedOn",
                table: "MissionIdeas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CreatedOn",
                table: "Clubs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UpdatedOn",
                table: "Clubs",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MissionIdeas");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "MissionIdeas");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Clubs");
        }
    }
}
