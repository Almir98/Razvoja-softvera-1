using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaturskiIspit_SkolskaGodina_SkolskaGodinaID",
                table: "MaturskiIspit");

            migrationBuilder.DropIndex(
                name: "IX_MaturskiIspit_SkolskaGodinaID",
                table: "MaturskiIspit");

            migrationBuilder.DropColumn(
                name: "SkolskaGodinaID",
                table: "MaturskiIspit");

            migrationBuilder.AddColumn<string>(
                name: "SkolskaGodina",
                table: "MaturskiIspit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkolskaGodina",
                table: "MaturskiIspit");

            migrationBuilder.AddColumn<int>(
                name: "SkolskaGodinaID",
                table: "MaturskiIspit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_SkolskaGodinaID",
                table: "MaturskiIspit",
                column: "SkolskaGodinaID");

            migrationBuilder.AddForeignKey(
                name: "FK_MaturskiIspit_SkolskaGodina_SkolskaGodinaID",
                table: "MaturskiIspit",
                column: "SkolskaGodinaID",
                principalTable: "SkolskaGodina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
