

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class Prestamo
    {
        [Key]

        public int IdPrestamo { get; set; }
        public decimal Monto { get; set; }
        public decimal Deuda { get; set; }

        public string NumeroPrestamo { get; set; }

        public string NumeroProducto { get; set; }

        // Relación con Usuario
        public string UserId { get; set; }

        // Relación con Pagos
        public ICollection<Pago> Pagos { get; set; }
    }
}
