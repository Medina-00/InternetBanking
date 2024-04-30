

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Producto
{
    public class SaveProductoViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Id de Usuario")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Debe de Ingresar el Tipo Del Producto")]
        public string Tipo { get; set; }
    }
}
