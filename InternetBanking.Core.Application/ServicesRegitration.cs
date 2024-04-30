using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    public static class ServicesRegitration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<ICuentaAhorroService, CuentaAhorroService>();
            services.AddTransient<IPrestamoService, PrestamoService>();
            services.AddTransient<ITarjetaCreditoService, TarjetaCreditoService>();
            services.AddTransient<IBeneficiarioService, BeneficiarioService>();
            services.AddTransient<IPagoService, PagoService>();
            services.AddTransient<IAvenceEfectivoService, AvanceEfectivoService>();
            services.AddTransient<ITransferenciaService, TransferenciaService>();


            #endregion


        }

    }
}
