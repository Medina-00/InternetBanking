
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Beneficiario
{
    public class SaveBeneficiarioViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe de Ingresar el Numero de cuenta")]
        public string NumeroCuenta { get; set; }
    }
}
