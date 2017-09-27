using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tully.Api.Data.Migrations
{
    public partial class DesafioFourSquareAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Desafio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Desafio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Desafio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Desafio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Desafio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Desafio");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Desafio");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Desafio");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Desafio");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Desafio");
        }
    }
}
