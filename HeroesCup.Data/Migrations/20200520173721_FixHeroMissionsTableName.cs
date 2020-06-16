using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class FixHeroMissionsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_HeroMission_Heroes_HeroId",
            //    table: "HeroMission");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_HeroMission_Missions_MissionId",
            //    table: "HeroMission");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_HeroMission",
            //    table: "HeroMission");

            //migrationBuilder.RenameTable(
            //    name: "HeroMission",
            //    newName: "HeroMissions");

            //migrationBuilder.RenameIndex(
            //    name: "IX_HeroMission_MissionId",
            //    table: "HeroMissions",
            //    newName: "IX_HeroMissions_MissionId");

        //    migrationBuilder.AddPrimaryKey(
        //        name: "PK_HeroMissions",
        //        table: "HeroMissions",
        //        columns: new[] { "HeroId", "MissionId" });

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_HeroMissions_Heroes_HeroId",
        //        table: "HeroMissions",
        //        column: "HeroId",
        //        principalTable: "Heroes",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_HeroMissions_Missions_MissionId",
        //        table: "HeroMissions",
        //        column: "MissionId",
        //        principalTable: "Missions",
        //        principalColumn: "Id",
        //        onDelete: ReferentialAction.Cascade);
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_HeroMissions_Heroes_HeroId",
        //        table: "HeroMissions");

        //    migrationBuilder.DropForeignKey(
        //        name: "FK_HeroMissions_Missions_MissionId",
        //        table: "HeroMissions");

        //    migrationBuilder.DropPrimaryKey(
        //        name: "PK_HeroMissions",
        //        table: "HeroMissions");

        //    migrationBuilder.RenameTable(
        //        name: "HeroMissions",
        //        newName: "HeroMission");

            //migrationBuilder.RenameIndex(
            //    name: "IX_HeroMissions_MissionId",
            //    table: "HeroMission",
            //    newName: "IX_HeroMission_MissionId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_HeroMission",
            //    table: "HeroMission",
            //    columns: new[] { "HeroId", "MissionId" });

            //migrationBuilder.AddForeignKey(
            //    name: "FK_HeroMission_Heroes_HeroId",
            //    table: "HeroMission",
            //    column: "HeroId",
            //    principalTable: "Heroes",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_HeroMission_Missions_MissionId",
            //    table: "HeroMission",
            //    column: "MissionId",
            //    principalTable: "Missions",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}