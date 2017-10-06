using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tully.Api.Data.Migrations
{
    public partial class RelacionamentoFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AtualizadoEm = table.Column<DateTime>(nullable: true),
                    CriadoEm = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    DesafioId = table.Column<int>(nullable: false),
                    FotoUrl = table.Column<string>(nullable: false),
                    RemovidoEm = table.Column<DateTime>(nullable: true),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foto_Desafio_DesafioId",
                        column: x => x.DesafioId,
                        principalTable: "Desafio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Foto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relacionamentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CriadoEm = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    RemovidoEm = table.Column<DateTime>(nullable: false),
                    SeguidoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relacionamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relacionamentos_Usuario_SeguidoId",
                        column: x => x.SeguidoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relacionamentos_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foto_DesafioId",
                table: "Foto",
                column: "DesafioId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_UsuarioId",
                table: "Foto",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacionamentos_SeguidoId",
                table: "Relacionamentos",
                column: "SeguidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Relacionamentos_UsuarioId",
                table: "Relacionamentos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropTable(
                name: "Relacionamentos");
        }
    }
}
