
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using System;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class PrestamoRepository : GenericRepository<Prestamo>, IPrestamo
    {
        private readonly ApplicationContext applicationContext;

        public PrestamoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public override async Task<Prestamo> AddAsync(Prestamo t)
        {
            await applicationContext.Set<Prestamo>().AddAsync(t);
            t.NumeroPrestamo = GenerarNumeroCuenta();
            t.Monto = t.Monto;
            t.Deuda = t.Monto;
            await applicationContext.SaveChangesAsync();
            return t;
        }

        public string GenerarNumeroCuenta()
        {
            string numeroCuenta;
            Random random = new Random();
            bool cuentaUnica = false;

            do
            {
                // Generar un número de cuenta aleatorio
                numeroCuenta = random.Next(100000000, 999999999).ToString();

                // Verificar si el número de cuenta ya existe en el sistema
                cuentaUnica = applicationContext.CuentasAhorro.Any(c => c.NumeroCuenta == numeroCuenta);
            } while (cuentaUnica);

            return numeroCuenta;
        }
    }
}
