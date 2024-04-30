

namespace InternetBanking.Core.Application.ViewModels.Prestamo
{
    public class PrestamoViewModel
    {
        public int IdPrestamo { get; set; }
        public decimal Monto { get; set; }
        public decimal Deuda { get; set; }

        public string NumeroPrestamo { get; set; }
        public string NumeroProducto { get; set; }


        // Relación con Usuario
        public string UserId { get; set; }
    }
}
