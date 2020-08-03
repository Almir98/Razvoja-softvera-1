using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_Ispit_asp.net_core.Migrations
{
    public partial class s : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdrzaniCasDetalji");

            migrationBuilder.CreateTable(
                name: "OdrzaniDetalji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: false),
                    OdjeljenjeStavkaID = table.Column<int>(nullable: false),
                    OdrzaniCasID = table.Column<int>(nullable: false),
                    Opravdano = table.Column<bool>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniDetalji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OdrzaniDetalji_OdjeljenjeStavka_OdjeljenjeStavkaID",
                        column: x => x.OdjeljenjeStavkaID,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OdrzaniDetalji_OdrzaniCas_OdrzaniCasID",
                        column: x => x.OdrzaniCasID,
                        principalTable: "OdrzaniCas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniDetalji_OdjeljenjeStavkaID",
                table: "OdrzaniDetalji",
                column: "OdjeljenjeStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniDetalji_OdrzaniCasID",
                table: "OdrzaniDetalji",
                column: "OdrzaniCasID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdrzaniDetalji");

            migrationBuilder.CreateTable(
                name: "OdrzaniCasDetalji",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Napomena = table.Column<string>(nullable: true),
                    Ocjena = table.Column<int>(nullable: true),
                    OdjeljenjeStavkaID = table.Column<int>(nullable: false),
                    OdrzaniCasID = table.Column<int>(nullable: false),
                    OpravdanoOdsutan = table.Column<bool>(nullable: false),
                    Prisutan = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCasDetalji", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasDetalji_OdjeljenjeStavka_OdjeljenjeStavkaID",
                        column: x => x.OdjeljenjeStavkaID,
                        principalTable: "OdjeljenjeStavka",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasDetalji_OdrzaniCas_OdrzaniCasID",
                        column: x => x.OdrzaniCasID,
                        principalTable: "OdrzaniCas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalji_OdjeljenjeStavkaID",
                table: "OdrzaniCasDetalji",
                column: "OdjeljenjeStavkaID");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalji_OdrzaniCasID",
                table: "OdrzaniCasDetalji",
                column: "OdrzaniCasID");
        }
    }
}
