using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LABClothingCollection.API.Migrations
{
    /// <inheritdoc />
    public partial class CriandoColecao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11);

            migrationBuilder.CreateTable(
                name: "Colecao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeColecao = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colecao_ResponsavelId",
                table: "Colecao",
                column: "ResponsavelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colecao");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "VARCHAR(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "VARCHAR(11)",
                oldMaxLength: 11,
                oldNullable: true);
        }
    }
}
