

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class CuentaAhorro
    {
        [Key]

        public int IdCuentaAhorro { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public bool EsPrincipal { get; set; }

        public string NumeroProducto { get; set; }

        // Relación con Usuario
        public string UserId { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }

        public ICollection<AvanceEfectivo> AvanceEfectivos { get; set; }

        public ICollection<Transferencia> transferencias { get; set; }


    }
}
