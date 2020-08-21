using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class AddIndicesForMissionAndMissionIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Missions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MissionIdeas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_Title",
                table: "Missions",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionIdeas_Title",
                table: "MissionIdeas",
                column: "Title",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Missions_Title",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_MissionIdeas_Title",
                table: "MissionIdeas");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Missions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MissionIdeas",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
