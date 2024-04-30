

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class TarjetaCredito
    {
        [Key]

        public int IdTarjetaCredito { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal Deuda { get; set; }

        public bool EsPrincipal { get; set; }

        public string NumeroProducto { get; set; }


        // Relación con Usuario
        public string UserId { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }

        public ICollection<AvanceEfectivo> AvanceEfectivos { get; set; }

    }
}
