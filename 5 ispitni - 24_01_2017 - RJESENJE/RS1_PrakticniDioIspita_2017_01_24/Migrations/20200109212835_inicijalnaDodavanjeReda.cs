using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_PrakticniDioIspita_2017_01_24.Migrations
{
    public partial class inicijalnaDodavanjeReda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "odrzaniCasID",
                table: "OdrzaniCasDetalj",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalj_odrzaniCasID",
                table: "OdrzaniCasDetalj",
                column: "odrzaniCasID");

            migrationBuilder.AddForeignKey(
                name: "FK_OdrzaniCasDetalj_OdrzaniCas_odrzaniCasID",
                table: "OdrzaniCasDetalj",
                column: "odrzaniCasID",
                principalTable: "OdrzaniCas",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OdrzaniCasDetalj_OdrzaniCas_odrzaniCasID",
                table: "OdrzaniCasDetalj");

            migrationBuilder.DropIndex(
                name: "IX_OdrzaniCasDetalj_odrzaniCasID",
                table: "OdrzaniCasDetalj");

            migrationBuilder.DropColumn(
                name: "odrzaniCasID",
                table: "OdrzaniCasDetalj");
        }
    }
}
