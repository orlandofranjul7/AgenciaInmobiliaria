﻿// <auto-generated />
using System;
using AgenciaInmobiliaria_API.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgenciaInmobiliaria_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240117141807_InsercionDeDatos")]
    partial class InsercionDeDatos
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AgenciaInmobiliaria_API.Modelos.Inmuebles", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<decimal>("Tarifa")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("Inmuebles");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Amenidad = "",
                            Detalle = "Detlle de la Villa",
                            FechaActualizacion = new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7986),
                            FechaCreacion = new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7977),
                            ImagenUrl = "",
                            MetrosCuadrados = 120,
                            Nombre = "Villa en La Romana",
                            Ocupantes = 10,
                            Tarifa = 200m
                        },
                        new
                        {
                            id = 2,
                            Amenidad = "",
                            Detalle = "Detlle de la Villa",
                            FechaActualizacion = new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7989),
                            FechaCreacion = new DateTime(2024, 1, 17, 10, 18, 7, 310, DateTimeKind.Local).AddTicks(7988),
                            ImagenUrl = "",
                            MetrosCuadrados = 120,
                            Nombre = "Villa en La Romana",
                            Ocupantes = 10,
                            Tarifa = 200m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
