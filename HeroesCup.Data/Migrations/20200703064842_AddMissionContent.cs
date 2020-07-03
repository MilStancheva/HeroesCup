using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class AddMissionContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Missions");

            migrationBuilder.CreateTable(
                name: "MissionContents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    What = table.Column<string>(nullable: true),
                    When = table.Column<string>(nullable: true),
                    Where = table.Column<string>(nullable: true),
                    Equipment = table.Column<string>(nullable: true),
                    Why = table.Column<string>(nullable: true),
                    MissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionContents_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionContents_MissionId",
                table: "MissionContents",
                column: "MissionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionContents");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Missions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
