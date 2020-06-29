using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace RS1_PrakticniDioIspita_2017_01_24.Migrations
{
    public partial class inicijalna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nastavnik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ucenik",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ucenik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavnikID = table.Column<int>(nullable: true),
                    Oznaka = table.Column<string>(nullable: true),
                    Razred = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odjeljenje_Nastavnik_NastavnikID",
                        column: x => x.NastavnikID,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Angazovan",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NatavnikID = table.Column<int>(nullable: true),
                    OdjeljenjeID = table.Column<int>(nullable: true),
                    PredmetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angazovan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Angazovan_Nastavnik_NatavnikID",
                        column: x => x.NatavnikID,
                        principalTable: "Nastavnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Angazovan_Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Angazovan_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UpisUOdjeljenje",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrojUDnevniku = table.Column<int>(nullable: false),
                    OdjeljenjeID = table.Column<int>(nullable: false),
                    UcenikID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpisUOdjeljenje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Odjeljenje_OdjeljenjeID",
                        column: x => x.OdjeljenjeID,
                        principalTable: "Odjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UpisUOdjeljenje_Ucenik_UcenikID",
                        column: x => x.UcenikID,
                        principalTable: "Ucenik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OdrzaniCas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AngazovanID = table.Column<int>(nullable: true),
                    datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzaniCas_Angazovan_AngazovanID",
                        column: x => x.AngazovanID,
                        principalTable: "Angazovan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OdrzaniCasDetalj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ocjena = table.Column<int>(nullable: true),
                    Odsutan = table.Column<bool>(nullable: false),
                    OpravdanoOdsutan = table.Column<bool>(nullable: true),
                    UpisUOdjeljenjeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OdrzaniCasDetalj", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OdrzaniCasDetalj_UpisUOdjeljenje_UpisUOdjeljenjeID",
                        column: x => x.UpisUOdjeljenjeID,
                        principalTable: "UpisUOdjeljenje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_NatavnikID",
                table: "Angazovan",
                column: "NatavnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_OdjeljenjeID",
                table: "Angazovan",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_Angazovan_PredmetID",
                table: "Angazovan",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Odjeljenje_NastavnikID",
                table: "Odjeljenje",
                column: "NastavnikID");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCas_AngazovanID",
                table: "OdrzaniCas",
                column: "AngazovanID");

            migrationBuilder.CreateIndex(
                name: "IX_OdrzaniCasDetalj_UpisUOdjeljenjeID",
                table: "OdrzaniCasDetalj",
                column: "UpisUOdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_OdjeljenjeID",
                table: "UpisUOdjeljenje",
                column: "OdjeljenjeID");

            migrationBuilder.CreateIndex(
                name: "IX_UpisUOdjeljenje_UcenikID",
                table: "UpisUOdjeljenje",
                column: "UcenikID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OdrzaniCas");

            migrationBuilder.DropTable(
                name: "OdrzaniCasDetalj");

            migrationBuilder.DropTable(
                name: "Angazovan");

            migrationBuilder.DropTable(
                name: "UpisUOdjeljenje");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "Odjeljenje");

            migrationBuilder.DropTable(
                name: "Ucenik");

            migrationBuilder.DropTable(
                name: "Nastavnik");
        }
    }
}
