using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Transferencia
{
    public class SaveTransferenciaViewModel
    {

        [Required(ErrorMessage = "Debe de Ingresar el Monto")]
        public decimal Monto { get; set; }


        [Required(ErrorMessage = "Debe de Ingresar la cuenta de ahorro de salida")]
        public int IdCuentaAhorro { get; set; }

        public ICollection<CuentaAhorroViewModel> ?CuentaOrigen { get; set; }
        [Required(ErrorMessage = "Debe de Ingresar la cuenta de ahorro de entrada")]
        public int CuentaDestinoId { get; set; }
        public ICollection<CuentaAhorroViewModel> ?CuentaDestino { get; set; }
    }
}
