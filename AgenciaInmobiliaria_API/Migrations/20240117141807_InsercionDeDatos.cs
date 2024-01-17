using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgenciaInmobiliaria_API.Migrations
{
    /// <inheritdoc />
    public partial class InsercionDeDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inmuebles",
                columns: new[] { "id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detlle de la Villa", new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7986), new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7977), "", 120, "Villa en La Romana", 10, 200m },
                    { 2, "", "Detlle de la Villa", new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7989), new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7988), "", 120, "Villa en La Romana", 10, 200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
