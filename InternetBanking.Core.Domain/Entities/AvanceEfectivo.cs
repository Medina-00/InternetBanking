

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class AvanceEfectivo
    {
        [Key]
        public int IdAvanceEfectivo { get; set; }
        public decimal Monto { get; set; }
        public decimal Interes { get; set; }

        public DateTime fechaPago { get; set; } = DateTime.Now;


        // Relación con Tarjeta de Crédito
        public int IdTarjetaCredito { get; set; }
        public TarjetaCredito TarjetaCredito { get; set; }

        // Relación con Cuenta de Ahorro
        public int IdCuentaAhorro { get; set; }
        public CuentaAhorro CuentaAhorro { get; set; }
    }
}
