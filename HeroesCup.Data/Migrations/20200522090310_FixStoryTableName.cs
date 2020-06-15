using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class FixStoryTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Story_Missions_MissionId",
            //    table: "Story");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_StoryImages_Story_StoryId",
            //    table: "StoryImages");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Story",
            //    table: "Story");

            //migrationBuilder.RenameTable(
            //    name: "Story",
            //    newName: "Stories");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Story_MissionId",
            //    table: "Stories",
            //    newName: "IX_Stories_MissionId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Stories",
            //    table: "Stories",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Stories_Missions_MissionId",
            //    table: "Stories",
            //    column: "MissionId",
            //    principalTable: "Missions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StoryImages_Stories_StoryId",
            //    table: "StoryImages",
            //    column: "StoryId",
            //    principalTable: "Stories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Stories_Missions_MissionId",
            //    table: "Stories");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_StoryImages_Stories_StoryId",
            //    table: "StoryImages");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Stories",
            //    table: "Stories");

            //migrationBuilder.RenameTable(
            //    name: "Stories",
            //    newName: "Story");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Stories_MissionId",
            //    table: "Story",
            //    newName: "IX_Story_MissionId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Story",
            //    table: "Story",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Story_Missions_MissionId",
            //    table: "Story",
            //    column: "MissionId",
            //    principalTable: "Missions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_StoryImages_Story_StoryId",
            //    table: "StoryImages",
            //    column: "StoryId",
            //    principalTable: "Story",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}