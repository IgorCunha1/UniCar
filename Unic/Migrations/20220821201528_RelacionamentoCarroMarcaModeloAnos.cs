using Microsoft.EntityFrameworkCore.Migrations;

namespace Unic.Migrations
{
    public partial class RelacionamentoCarroMarcaModeloAnos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Carro");

            migrationBuilder.DropColumn(
                name: "Modelo",
                table: "Carro");

            migrationBuilder.RenameColumn(
                name: "AnoModelo",
                table: "Carro",
                newName: "ModeloId");

            migrationBuilder.AddColumn<int>(
                name: "AnoModeloId",
                table: "Carro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MarcaId",
                table: "Carro",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Anos_AnoModeloId",
                table: "Carro",
                column: "AnoModeloId",
                principalTable: "Anos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Marca_MarcaId",
                table: "Carro",
                column: "MarcaId",
                principalTable: "Marca",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carro_Modelo_ModeloId",
                table: "Carro",
                column: "ModeloId",
                principalTable: "Modelo",
                principalColumn: "codigo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Anos_AnoModeloId",
                table: "Carro");

            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Marca_MarcaId",
                table: "Carro");

            migrationBuilder.DropForeignKey(
                name: "FK_Carro_Modelo_ModeloId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_AnoModeloId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_MarcaId",
                table: "Carro");

            migrationBuilder.DropIndex(
                name: "IX_Carro_ModeloId",
                table: "Carro");

            migrationBuilder.DropColumn(
                name: "AnoModeloId",
                table: "Carro");

            migrationBuilder.DropColumn(
                name: "MarcaId",
                table: "Carro");

            migrationBuilder.RenameColumn(
                name: "ModeloId",
                table: "Carro",
                newName: "AnoModelo");

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Carro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Modelo",
                table: "Carro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
