using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "DataNascimento", "Documento", "Email", "Genero", "NomeCompleto", "Status", "Telefone", "Tipo" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "23188028075", "marianadcgd@hotmail.com", "Feminino", "Mariana De Carvalho Gonçaves Daruix", 1, "11996448176", 0 },
                    { 2, new DateTime(1964, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "25527501764", "levi.murilo.porto@bidoul.eng.br", "Masculino", "Levi Murilo Caio Porto", 0, "11995624145", 2 },
                    { 3, new DateTime(2001, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "83934472000149", "isabella_marina_nunes@uol.com.br", "Feminino", "Isabella Marina Nunes", 1, "11991551385", 1 },
                    { 4, new DateTime(1984, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "81994451300", "alinebrendafreitas@yahoo.com", "Feminino", "Aline Brenda Freitas", 1, "11981604710", 3 },
                    { 5, new DateTime(1970, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "78140716669", "veraisadoracortereal@mtic.net.br", "Feminino", "Vera Isadora Corte Real", 0, "11994063594", 1 }
                });

            migrationBuilder.InsertData(
                table: "Colecao",
                columns: new[] { "Id", "AnoLancamento", "Estacao", "Marca", "Nome", "Orcamento", "ResponsavelId", "StatusSistema" },
                values: new object[,]
                {
                    { 1, 2022, 1, "CeA", "Colecao Inverno", 145987.98m, 3, 1 },
                    { 2, 2050, 3, "MARISA", "Colecao Verao", 45398.01m, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Modelo",
                columns: new[] { "Id", "ColecaoId", "Layout", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, 2, "Modelo A Colecao Inverno", 5 },
                    { 2, 1, 1, "Modelo B Colecao Inverno", 7 },
                    { 3, 1, 3, "Modelo C Colecao Inverno", 1 },
                    { 4, 2, 1, "Modelo A Colecao VERAO", 1 },
                    { 5, 2, 3, "Modelo B Colecao VERAO", 2 }
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
