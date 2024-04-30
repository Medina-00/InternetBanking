﻿// <auto-generated />
using System;
using InternetBanking.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240325031057_thrirdMigration")]
    partial class thrirdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.AvanceEfectivo", b =>
                {
                    b.Property<int>("IdAvanceEfectivo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAvanceEfectivo"));

                    b.Property<int>("IdCuentaAhorro")
                        .HasColumnType("int");

                    b.Property<int>("IdTarjetaCredito")
                        .HasColumnType("int");

                    b.Property<decimal>("Interes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdAvanceEfectivo");

                    b.HasIndex("IdCuentaAhorro");

                    b.HasIndex("IdTarjetaCredito");

                    b.ToTable("AvancesEfectivo", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Beneficiario", b =>
                {
                    b.Property<int>("IdBeneficiario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdBeneficiario"));

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserIdBeneficiario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdBeneficiario");

                    b.ToTable("Beneficiarios", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CuentaAhorro", b =>
                {
                    b.Property<int>("IdCuentaAhorro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCuentaAhorro"));

                    b.Property<bool>("EsPrincipal")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroCuenta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Saldo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCuentaAhorro");

                    b.ToTable("CuentasAhorro", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Pago", b =>
                {
                    b.Property<int>("IdPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPago"));

                    b.Property<int?>("BeneficiarioId")
                        .HasColumnType("int");

                    b.Property<int?>("CuentaAhorroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroCuenta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrestamoId")
                        .HasColumnType("int");

                    b.Property<int?>("TarjetaCreditoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("fechaPago")
                        .HasColumnType("datetime2");

                    b.HasKey("IdPago");

                    b.HasIndex("BeneficiarioId");

                    b.HasIndex("CuentaAhorroId");

                    b.HasIndex("PrestamoId");

                    b.HasIndex("TarjetaCreditoId");

                    b.ToTable("Pagos", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Prestamo", b =>
                {
                    b.Property<int>("IdPrestamo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPrestamo"));

                    b.Property<decimal>("Deuda")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroPrestamo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPrestamo");

                    b.ToTable("Prestamos", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Producto", b =>
                {
                    b.Property<int>("IDProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDProducto"));

                    b.Property<string>("Numero9Digitos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDProducto");

                    b.ToTable("Productos", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.TarjetaCredito", b =>
                {
                    b.Property<int>("IdTarjetaCredito")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTarjetaCredito"));

                    b.Property<decimal>("Deuda")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("EsPrincipal")
                        .HasColumnType("bit");

                    b.Property<decimal>("LimiteCredito")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NumeroProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroTarjeta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTarjetaCredito");

                    b.ToTable("TarjetasCredito", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Transferencia", b =>
                {
                    b.Property<int>("IdTransferencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransferencia"));

                    b.Property<int?>("CuentaAhorroIdCuentaAhorro")
                        .HasColumnType("int");

                    b.Property<int>("CuentaDestinoId")
                        .HasColumnType("int");

                    b.Property<int>("IdCuentaAhorro")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("fechaPago")
                        .HasColumnType("datetime2");

                    b.HasKey("IdTransferencia");

                    b.HasIndex("CuentaAhorroIdCuentaAhorro");

                    b.HasIndex("CuentaDestinoId");

                    b.HasIndex("IdCuentaAhorro");

                    b.ToTable("Transferencias", (string)null);
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.AvanceEfectivo", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.CuentaAhorro", "CuentaAhorro")
                        .WithMany("AvanceEfectivos")
                        .HasForeignKey("IdCuentaAhorro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.TarjetaCredito", "TarjetaCredito")
                        .WithMany("AvanceEfectivos")
                        .HasForeignKey("IdTarjetaCredito")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CuentaAhorro");

                    b.Navigation("TarjetaCredito");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Pago", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.Beneficiario", "Beneficiario")
                        .WithMany()
                        .HasForeignKey("BeneficiarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InternetBanking.Core.Domain.Entities.CuentaAhorro", "CuentaAhorro")
                        .WithMany("Pagos")
                        .HasForeignKey("CuentaAhorroId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InternetBanking.Core.Domain.Entities.Prestamo", "Prestamo")
                        .WithMany("Pagos")
                        .HasForeignKey("PrestamoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InternetBanking.Core.Domain.Entities.TarjetaCredito", "TarjetaCredito")
                        .WithMany("Pagos")
                        .HasForeignKey("TarjetaCreditoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Beneficiario");

                    b.Navigation("CuentaAhorro");

                    b.Navigation("Prestamo");

                    b.Navigation("TarjetaCredito");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Transferencia", b =>
                {
                    b.HasOne("InternetBanking.Core.Domain.Entities.CuentaAhorro", null)
                        .WithMany("transferencias")
                        .HasForeignKey("CuentaAhorroIdCuentaAhorro");

                    b.HasOne("InternetBanking.Core.Domain.Entities.CuentaAhorro", "CuentaDestino")
                        .WithMany()
                        .HasForeignKey("CuentaDestinoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetBanking.Core.Domain.Entities.CuentaAhorro", "CuentaOrigen")
                        .WithMany()
                        .HasForeignKey("IdCuentaAhorro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CuentaDestino");

                    b.Navigation("CuentaOrigen");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.CuentaAhorro", b =>
                {
                    b.Navigation("AvanceEfectivos");

                    b.Navigation("Pagos");

                    b.Navigation("transferencias");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.Prestamo", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("InternetBanking.Core.Domain.Entities.TarjetaCredito", b =>
                {
                    b.Navigation("AvanceEfectivos");

                    b.Navigation("Pagos");
                });
#pragma warning restore 612, 618
        }
    }
}
