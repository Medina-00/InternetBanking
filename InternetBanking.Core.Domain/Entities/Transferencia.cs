

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class Transferencia
    {
        [Key]

        public int IdTransferencia { get; set; }
        public decimal Monto { get; set; }

        public DateTime fechaPago { get; set; } = DateTime.Now;


        // Relación con Cuenta de Origen
        public int IdCuentaAhorro { get; set; }

        public CuentaAhorro CuentaOrigen { get; set; }

        // Relación con Cuenta de Destino
        public int CuentaDestinoId { get; set; }
        public CuentaAhorro CuentaDestino { get; set; }
    }
}
