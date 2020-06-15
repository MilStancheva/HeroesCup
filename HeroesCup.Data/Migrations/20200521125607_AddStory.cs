using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class AddStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    MissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryImages",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    StoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryImages", x => new { x.StoryId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_StoryImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryImages_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_MissionId",
                table: "Stories",
                column: "MissionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StoryImages_ImageId",
                table: "StoryImages",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryImages");

            migrationBuilder.DropTable(
                name: "Stories");
        }
    }
}