using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaInmobiliaria_API.Migrations
{
    /// <inheritdoc />
    public partial class CambioTipoDeDatoTarifa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Tarifa",
                table: "Inmuebles",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tarifa",
                table: "Inmuebles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
