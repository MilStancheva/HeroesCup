using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class SchoolClubToClub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_SchoolClubs_SchoolClubId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_SchoolClubs_SchoolClubId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "SchoolClubs");

            migrationBuilder.DropIndex(
                name: "IX_Missions_SchoolClubId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_SchoolClubId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SchoolClubId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "SchoolClubId",
                table: "Heroes");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Missions",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Heroes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    OrganizationName = table.Column<string>(nullable: true),
                    OrganizationType = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_ClubId",
                table: "Missions",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_ClubId",
                table: "Heroes",
                column: "ClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Clubs_ClubId",
                table: "Heroes",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Clubs_ClubId",
                table: "Missions",
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

            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Clubs_ClubId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Missions_ClubId",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_ClubId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Heroes");

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolClubId",
                table: "Missions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SchoolClubId",
                table: "Heroes",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SchoolClubs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Location = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Name = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    SchoolName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SchoolType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolClubs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missions_SchoolClubId",
                table: "Missions",
                column: "SchoolClubId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SchoolClubId",
                table: "Heroes",
                column: "SchoolClubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_SchoolClubs_SchoolClubId",
                table: "Heroes",
                column: "SchoolClubId",
                principalTable: "SchoolClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_SchoolClubs_SchoolClubId",
                table: "Missions",
                column: "SchoolClubId",
                principalTable: "SchoolClubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}