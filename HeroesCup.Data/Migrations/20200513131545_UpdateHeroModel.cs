using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class UpdateHeroModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Clubs_ClubId",
                table: "Heroes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "HeroMission",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Heroes",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Clubs_ClubId",
                table: "Heroes",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Clubs_ClubId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "HeroMission");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClubId",
                table: "Heroes",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Clubs_ClubId",
                table: "Heroes",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
