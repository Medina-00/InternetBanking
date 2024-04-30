

using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;

namespace InternetBanking.Core.Application.ViewModels.Producto
{
    public class ProductoViewModel
    {
        
        public int IDProducto { get; set; }
        public string UserId { get; set; }
        public string Tipo { get; set; }
        public string Numero9Digitos { get; set; }

        public ICollection<CuentaAhorroViewModel> cuentaAhorros { get; set; }
        public ICollection<PrestamoViewModel> prestamos { get; set; }

        public ICollection<TarjetaCreditoViewModel> TarjetaCreditos { get; set; }

    }
}
