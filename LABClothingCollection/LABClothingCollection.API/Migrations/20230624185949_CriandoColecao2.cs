using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LABClothingCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoColecao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeColecao",
                table: "Colecao");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Colecao",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_Nome",
                table: "Colecao",
                column: "Nome",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Colecao_Nome",
                table: "Colecao");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Colecao");

            migrationBuilder.AddColumn<string>(
                name: "NomeColecao",
                table: "Colecao",
                type: "VARCHAR(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
