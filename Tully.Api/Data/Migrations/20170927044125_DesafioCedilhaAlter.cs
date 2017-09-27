using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tully.Api.Data.Migrations
{
    public partial class DesafioCedilhaAlter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereço",
                table: "Desafio",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "Descrição",
                table: "Desafio",
                newName: "Descricao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "Desafio",
                newName: "Endereço");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Desafio",
                newName: "Descrição");
        }
    }
}
