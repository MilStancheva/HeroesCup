using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class AddMission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    StartDate = table.Column<long>(nullable: false),
                    EndDate = table.Column<long>(nullable: false),
                    SchoolYear = table.Column<int>(nullable: false),
                    Stars = table.Column<int>(nullable: false),
                    SchoolClubId = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    TimeheroesUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_SchoolClubs_SchoolClubId",
                        column: x => x.SchoolClubId,
                        principalTable: "SchoolClubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeroMission",
                columns: table => new
                {
                    HeroId = table.Column<Guid>(nullable: false),
                    MissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroMission", x => new { x.HeroId, x.MissionId });
                    table.ForeignKey(
                        name: "FK_HeroMission_Heroes_HeroId",
                        column: x => x.HeroId,
                        principalTable: "Heroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroMission_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroMission_MissionId",
                table: "HeroMission",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_SchoolClubId",
                table: "Missions",
                column: "SchoolClubId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroMission");

            migrationBuilder.DropTable(
                name: "Missions");
        }
    }
}