

using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<AvanceEfectivo> AvancesEfectivo { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<CuentaAhorro> CuentasAhorro { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<TarjetaCredito> TarjetasCredito { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Tables

            modelBuilder.Entity<AvanceEfectivo>().ToTable(nameof(AvancesEfectivo));
            modelBuilder.Entity<Beneficiario>().ToTable(nameof(Beneficiarios));
            modelBuilder.Entity<CuentaAhorro>().ToTable(nameof(CuentasAhorro));
            modelBuilder.Entity<Pago>().ToTable(nameof(Pagos));
            modelBuilder.Entity<Prestamo>().ToTable(nameof(Prestamos));
            modelBuilder.Entity<Producto>().ToTable(nameof(Productos));
            modelBuilder.Entity<TarjetaCredito>().ToTable(nameof(TarjetasCredito));
            modelBuilder.Entity<Transferencia>().ToTable(nameof(Transferencias));

            #endregion

            #region Primary Key
            modelBuilder.Entity<AvanceEfectivo>().HasKey(a => a.IdAvanceEfectivo);
            modelBuilder.Entity<Beneficiario>().HasKey(a => a.IdBeneficiario);
            modelBuilder.Entity<CuentaAhorro>().HasKey(a => a.IdCuentaAhorro);
            modelBuilder.Entity<Pago>().HasKey(a => a.IdPago);
            modelBuilder.Entity<Prestamo>().HasKey(a => a.IdPrestamo);
            modelBuilder.Entity<Producto>().HasKey(a => a.IDProducto);
            modelBuilder.Entity<TarjetaCredito>().HasKey(a => a.IdTarjetaCredito);
            modelBuilder.Entity<Transferencia>().HasKey(a => a.IdTransferencia);
            #endregion


            #region Relaciones

            modelBuilder.Entity<AvanceEfectivo>()
            .HasOne(ae => ae.TarjetaCredito)
            .WithMany(tc => tc.AvanceEfectivos)
            .HasForeignKey(ae => ae.IdTarjetaCredito);

            modelBuilder.Entity<AvanceEfectivo>()
                .HasOne(ae => ae.CuentaAhorro)
                .WithMany(ca => ca.AvanceEfectivos)
                .HasForeignKey(ae => ae.IdCuentaAhorro);

            modelBuilder.Entity<Pago>()
            .HasOne(p => p.CuentaAhorro)
            .WithMany(ca => ca.Pagos)
            .HasForeignKey(p => p.CuentaAhorroId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.TarjetaCredito)
                .WithMany(tc => tc.Pagos)
                .HasForeignKey(p => p.TarjetaCreditoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Prestamo)
                .WithMany(p => p.Pagos)
                .HasForeignKey(p => p.PrestamoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Beneficiario)
                .WithMany()
                .HasForeignKey(p => p.BeneficiarioId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transferencia>()
               .HasOne(t => t.CuentaOrigen)
               .WithMany()
               .HasForeignKey(t => t.IdCuentaAhorro);

            modelBuilder.Entity<Transferencia>()
                .HasOne(t => t.CuentaDestino)
                .WithMany()
                .HasForeignKey(t => t.CuentaDestinoId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
