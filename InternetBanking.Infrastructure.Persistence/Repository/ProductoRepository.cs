
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using System;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProducto
    {
        private readonly ApplicationContext applicationContext;

        public ProductoRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public override async Task<Producto> AddAsync(Producto t)
        {
            await applicationContext.Set<Producto>().AddAsync(t);
            t.Numero9Digitos = GenerarNumeroUnico();
            await applicationContext.SaveChangesAsync();
            return t;
        }
        public string GenerarNumeroUnico()
        {
            string numero;
            var random = new Random();
            do
            {
                // Generar un número aleatorio de 9 dígitos
                numero = random.Next(100000000, 999999999).ToString();
            } while (NumeroExiste(numero));

            return numero;
        }

        private bool NumeroExiste(string numero)
        {
            // Verificar si el número ya existe en la base de datos
            return applicationContext.Productos.Any(p => p.Numero9Digitos == numero);
        }
    }

}
