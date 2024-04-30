
namespace InternetBanking.Core.Application.ViewModels.CuentaAhorro
{
    public class CuentaAhorroViewModel
    {
        public int IdCuentaAhorro { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public bool EsPrincipal { get; set; }

        public string NumeroProducto { get; set; }


        // Relación con Usuario
        public string UserId { get; set; }
    }
}
