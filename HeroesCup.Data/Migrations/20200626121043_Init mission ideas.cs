using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class Initmissionideas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "HeroMissions");

            migrationBuilder.CreateTable(
                name: "MissionIdeas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    TimeheroesUrl = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionIdeas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionIdeaImages",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    MissionIdeaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionIdeaImages", x => new { x.MissionIdeaId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_MissionIdeaImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionIdeaImages_MissionIdeas_MissionIdeaId",
                        column: x => x.MissionIdeaId,
                        principalTable: "MissionIdeas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionIdeaImages_ImageId",
                table: "MissionIdeaImages",
                column: "ImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionIdeaImages");

            migrationBuilder.DropTable(
                name: "MissionIdeas");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HeroMissions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
