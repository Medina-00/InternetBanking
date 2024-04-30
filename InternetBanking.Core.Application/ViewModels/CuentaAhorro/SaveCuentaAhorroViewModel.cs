

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.CuentaAhorro
{
    public class SaveCuentaAhorroViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Saldo Inicial")]
        public decimal Saldo { get; set; }
        [Required(ErrorMessage = "Debe de Ingresar Si es principar")]
        public bool EsPrincipal { get; set; }
        [Required(ErrorMessage = "Debe de Ingresar el Id de Usuario")]
        public string UserId { get; set; }



    }
}
