using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class A : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PopravniIspit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false),
                    PredmetID = table.Column<int>(nullable: false),
                    SkolaID = table.Column<int>(nullable: false),
                    SkolskaGodinaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_Skola_SkolaID",
                        column: x => x.SkolaID,
                        principalTable: "Skola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PopravniIspit_SkolskaGodina_SkolskaGodinaID",
                        column: x => x.SkolskaGodinaID,
                        principalTable: "SkolskaGodina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PopravniIspitDetalji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OdjeljenjeStavkaID = table.Column<int>(nullable: false),
                    PopravniIspitID = table.Column<int>(nullable: false),
                    pristupio = table.Column<bool>(nullable: false),
                    rezultat = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopravniIspitDetalji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PopravniIspitDetalji_OdjeljenjeStavka_OdjeljenjeStavkaID",
                        column: x => x.OdjeljenjeStavkaID,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PopravniIspitDetalji_PopravniIspit_PopravniIspitID",
                        column: x => x.PopravniIspitID,
                        principalTable: "PopravniIspit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_OdjeljenjeID",
                table: "PopravniIspit",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_PredmetID",
                table: "PopravniIspit",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolaID",
                table: "PopravniIspit",
                column: "SkolaID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspit_SkolskaGodinaID",
                table: "PopravniIspit",
                column: "SkolskaGodinaID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitDetalji_OdjeljenjeStavkaID",
                table: "PopravniIspitDetalji",
                column: "OdjeljenjeStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_PopravniIspitDetalji_PopravniIspitID",
                table: "PopravniIspitDetalji",
                column: "PopravniIspitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopravniIspitDetalji");

            migrationBuilder.DropTable(
                name: "PopravniIspit");
        }
    }
}
