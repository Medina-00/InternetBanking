

using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class TarjetaCreditoRepository : GenericRepository<TarjetaCredito>, ITarjetaCredito
    {
        private readonly ApplicationContext applicationContext;

        public TarjetaCreditoRepository(ApplicationContext applicationContext) : base(applicationContext)
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

        public override async Task<TarjetaCredito> AddAsync(TarjetaCredito t)
        {
            await applicationContext.Set<TarjetaCredito>().AddAsync(t);
            t.NumeroTarjeta = GenerarNumeroCuenta();
            t.EsPrincipal = false;

            await applicationContext.SaveChangesAsync();
            return t;
        }
    }
}
