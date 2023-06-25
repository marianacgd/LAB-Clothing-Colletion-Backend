using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LABClothingCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    NomeCompleto = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    Genero = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Documento = table.Column<string>(type: "VARCHAR(18)", maxLength: 18, nullable: false),
                    Telefone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colecao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Marca = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Orcamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnoLancamento = table.Column<int>(type: "int", nullable: false),
                    Estacao = table.Column<int>(type: "int", nullable: false),
                    StatusSistema = table.Column<int>(type: "int", nullable: false),
                    ResponsavelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colecao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colecao_Usuario_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Layout = table.Column<int>(type: "int", nullable: false),
                    ColecaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelo_Colecao_ColecaoId",
                        column: x => x.ColecaoId,
                        principalTable: "Colecao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_Nome",
                table: "Colecao",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_ResponsavelId",
                table: "Colecao",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_ColecaoId",
                table: "Modelo",
                column: "ColecaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Nome",
                table: "Modelo",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modelo");

            migrationBuilder.DropTable(
                name: "Colecao");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
