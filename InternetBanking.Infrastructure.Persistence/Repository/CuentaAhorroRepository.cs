

using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class CuentaAhorroRepository : GenericRepository<CuentaAhorro>, ICuentaAhorro
    {
        private readonly ApplicationContext applicationContext;

        public CuentaAhorroRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
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

        public override async Task<CuentaAhorro> AddAsync(CuentaAhorro t)
        {
            await applicationContext.Set<CuentaAhorro>().AddAsync(t);
            t.NumeroCuenta = GenerarNumeroCuenta();
            await applicationContext.SaveChangesAsync();
            return t;
        }


    }
}
