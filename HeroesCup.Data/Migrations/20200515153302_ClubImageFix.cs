using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HeroesCup.Data.Migrations
{
    public partial class ClubImageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clubs_Images_LogoId",
                table: "Clubs");

            migrationBuilder.DropIndex(
                name: "IX_Clubs_LogoId",
                table: "Clubs");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubId",
                table: "Images",
                column: "ClubId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClubId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_LogoId",
                table: "Clubs",
                column: "LogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clubs_Images_LogoId",
                table: "Clubs",
                column: "LogoId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
