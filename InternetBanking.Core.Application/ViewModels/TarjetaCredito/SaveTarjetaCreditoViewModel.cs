
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.TarjetaCredito
{
    public class SaveTarjetaCreditoViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Id Del usuario")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Debe de Ingresar el Limite de la tarjeta")]

        public decimal LimiteCredito { get; set; }



    }
}
