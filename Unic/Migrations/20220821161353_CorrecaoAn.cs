using Microsoft.EntityFrameworkCore.Migrations;

namespace Unic.Migrations
{
    public partial class CorrecaoAn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Anos",
                table: "Anos");

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "Anos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Anos",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anos",
                table: "Anos",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Anos",
                table: "Anos");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Anos");

            migrationBuilder.AlterColumn<string>(
                name: "codigo",
                table: "Anos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anos",
                table: "Anos",
                column: "codigo");
        }
    }
}
