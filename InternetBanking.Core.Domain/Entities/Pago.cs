

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    
        public class Pago
        {

            [Key]

            public int IdPago { get; set; }
            public decimal Monto { get; set; }

            public string ?NumeroCuenta { get; set; }
            public DateTime fechaPago { get; set; } = DateTime.Now;
            // Relación con Cuenta de Ahorro
            public int? CuentaAhorroId { get; set; }
            public CuentaAhorro CuentaAhorro { get; set; }

            // Relación con Tarjeta de Crédito
            public int? TarjetaCreditoId { get; set; }
            public TarjetaCredito TarjetaCredito { get; set; }

            // Relación con Préstamo
            public int? PrestamoId { get; set; }
            public Prestamo Prestamo { get; set; }

            // Relación con Beneficiario
            public int? BeneficiarioId { get; set; }
            public Beneficiario Beneficiario { get; set; }
        }

    
}
