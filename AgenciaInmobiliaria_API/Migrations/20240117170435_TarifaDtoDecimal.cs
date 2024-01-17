using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaInmobiliaria_API.Migrations
{
    /// <inheritdoc />
    public partial class TarifaDtoDecimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 4, 35, 766, DateTimeKind.Local).AddTicks(1837), new DateTime(2024, 1, 17, 13, 4, 35, 766, DateTimeKind.Local).AddTicks(1825) });

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 17, 13, 4, 35, 766, DateTimeKind.Local).AddTicks(1839), new DateTime(2024, 1, 17, 13, 4, 35, 766, DateTimeKind.Local).AddTicks(1839) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7986), new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7977) });

            migrationBuilder.UpdateData(
                table: "Inmuebles",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "FechaCreacion" },
                values: new object[] { new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7989), new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7988) });
        }
    }
}
