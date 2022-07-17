using Microsoft.EntityFrameworkCore.Migrations;

namespace Unic.Migrations
{
    public partial class addplaca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Placa",
                table: "Carro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Placa",
                table: "Carro");
        }
    }
}
