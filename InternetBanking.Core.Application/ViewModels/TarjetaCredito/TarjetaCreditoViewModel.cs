
namespace InternetBanking.Core.Application.ViewModels.TarjetaCredito
{
    public class TarjetaCreditoViewModel
    {
        public int IdTarjetaCredito { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Deuda { get; set; }

        public bool EsPrincipal { get; set; }

        public string NumeroProducto { get; set; }

        // Relación con Usuario
        public string UserId { get; set; }
    }
}
