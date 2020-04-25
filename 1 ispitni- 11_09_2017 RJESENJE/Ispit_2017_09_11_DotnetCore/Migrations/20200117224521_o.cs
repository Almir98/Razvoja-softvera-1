using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Ispit_2017_09_11_DotnetCore.Migrations
{
    public partial class o : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ZakljucnoKrajGodine",
                table: "DodjeljenPredmet",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ZakljucnoKrajGodine",
                table: "DodjeljenPredmet",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
