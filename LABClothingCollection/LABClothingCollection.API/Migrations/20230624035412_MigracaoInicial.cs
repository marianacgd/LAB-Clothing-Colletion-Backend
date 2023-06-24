using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LABClothingCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
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
                    Telefone = table.Column<string>(type: "VARCHAR(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

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
                name: "Usuario");
        }
    }
}
