using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_20170621_v1.Migrations
{
    public partial class a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaturskiIspit",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    NastavnikID = table.Column<int>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Nastavnik_NastavnikID",
                        column: x => x.NastavnikID,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspit_Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "MaturskiIspitStavka",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Bodovi = table.Column<float>(nullable: true),
                    MaturskiIspitID = table.Column<int>(nullable: false),
                    Oslobodjen = table.Column<bool>(nullable: false),
                    UpisUOdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaturskiIspitStavka", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_MaturskiIspit_MaturskiIspitID",
                        column: x => x.MaturskiIspitID,
                        principalTable: "MaturskiIspit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaturskiIspitStavka_UpisUOdjeljenje_UpisUOdjeljenjeID",
                        column: x => x.UpisUOdjeljenjeID,
                        principalTable: "UpisUOdjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_NastavnikID",
                table: "MaturskiIspit",
                column: "NastavnikID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspit_OdjeljenjeID",
                table: "MaturskiIspit",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_MaturskiIspitID",
                table: "MaturskiIspitStavka",
                column: "MaturskiIspitID");

            migrationBuilder.CreateIndex(
                name: "IX_MaturskiIspitStavka_UpisUOdjeljenjeID",
                table: "MaturskiIspitStavka",
                column: "UpisUOdjeljenjeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaturskiIspitStavka");

            migrationBuilder.DropTable(
                name: "MaturskiIspit");
        }
    }
}
