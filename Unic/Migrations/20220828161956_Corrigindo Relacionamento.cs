using Microsoft.EntityFrameworkCore.Migrations;

namespace Unic.Migrations
{
    public partial class CorrigindoRelacionamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carro_AnoModeloId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_MarcaId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_ModeloId",
                table: "Carro");

            migrationBuilder.CreateIndex(
                name: "IX_Carro_AnoModeloId",
                table: "Carro",
                column: "AnoModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Carro_MarcaId",
                table: "Carro",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Carro_ModeloId",
                table: "Carro",
                column: "ModeloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Carro_AnoModeloId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_MarcaId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_ModeloId",
                table: "Carro");

            migrationBuilder.CreateIndex(
                name: "IX_Carro_AnoModeloId",
                table: "Carro",
                column: "AnoModeloId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carro_MarcaId",
                table: "Carro",
                column: "MarcaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carro_ModeloId",
                table: "Carro",
                column: "ModeloId",
                unique: true);
        }
    }
}
