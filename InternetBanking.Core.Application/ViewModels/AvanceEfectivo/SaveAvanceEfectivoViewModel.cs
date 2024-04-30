

using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.AvanceEfectivo
{
    public class SaveAvanceEfectivoViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Monto")]
        public decimal Monto { get; set; }
        public decimal Interes { get; set; }

        [Required(ErrorMessage = "Debe de Ingresar el la tarjeta de credito")]
        public int IdTarjetaCredito { get; set; }
        public ICollection<TarjetaCreditoViewModel> ?TarjetaCredito { get; set; }

        [Required(ErrorMessage = "Debe de Ingresar la cuenta de ahorro")]
        public int IdCuentaAhorro { get; set; }
        public ICollection<CuentaAhorroViewModel> ?CuentaAhorro { get; set; }
    }
}
