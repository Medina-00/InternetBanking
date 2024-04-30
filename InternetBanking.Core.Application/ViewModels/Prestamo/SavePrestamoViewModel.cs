

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Prestamo
{
    public class SavePrestamoViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Usuario Del Usuario")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe de Ingresar el Monto del Prestamo")]

        public decimal Monto { get; set; }


    }
}
