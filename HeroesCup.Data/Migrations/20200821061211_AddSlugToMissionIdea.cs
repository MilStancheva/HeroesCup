using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class AddSlugToMissionIdea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MissionIdeas_Title",
                table: "MissionIdeas");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MissionIdeas",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "MissionIdeas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionIdeas_Slug",
                table: "MissionIdeas",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MissionIdeas_Slug",
                table: "MissionIdeas");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "MissionIdeas");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MissionIdeas",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MissionIdeas_Title",
                table: "MissionIdeas",
                column: "Title",
                unique: true);
        }
    }
}
