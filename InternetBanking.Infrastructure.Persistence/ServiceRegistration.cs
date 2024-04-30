using InternetBanking.Core.Application.Interfaces;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using InternetBanking.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Contexts

            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));


            #endregion
             
            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAvenceEfectivo, AvanceEfectivoRepository>();
            services.AddTransient<IBeneficiario, BeneficiarioRepository>();
            services.AddTransient<ICuentaAhorro, CuentaAhorroRepository>();
            services.AddTransient<IPago, PagoRepository>();
            services.AddTransient<IPrestamo, PrestamoRepository>();
            services.AddTransient<IProducto, ProductoRepository>();
            services.AddTransient<ITarjetaCredito, TarjetaCreditoRepository>();
            services.AddTransient<ITransferencia, TransferenciaRepository>();
            #endregion
        }

    }
}
