using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tully.Api.Data.Migrations
{
    public partial class RelacionamentoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacionamentos_Usuario_SeguidoId",
                table: "Relacionamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacionamentos_Usuario_UsuarioId",
                table: "Relacionamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacionamentos_Usuario_SeguidoId",
                table: "Relacionamentos",
                column: "SeguidoId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacionamentos_Usuario_UsuarioId",
                table: "Relacionamentos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relacionamentos_Usuario_SeguidoId",
                table: "Relacionamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Relacionamentos_Usuario_UsuarioId",
                table: "Relacionamentos");

            migrationBuilder.AddForeignKey(
                name: "FK_Relacionamentos_Usuario_SeguidoId",
                table: "Relacionamentos",
                column: "SeguidoId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Relacionamentos_Usuario_UsuarioId",
                table: "Relacionamentos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
