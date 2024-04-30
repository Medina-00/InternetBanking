﻿

using InternetBanking.Core.Application.ViewModels.Beneficiario;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Pago
{
    public class SavePagoViewModel
    {
        [Required(ErrorMessage = "Debe de Ingresar el Monto")]

        public decimal Monto { get; set; }

        public string TipoPago { get; set; }

        public string? NumeroCuenta { get; set; }

        // Relación con Cuenta de Ahorro
        public int? CuentaAhorroId { get; set; }
        public ICollection<CuentaAhorroViewModel> CuentasAhorro { get; set; }

        // Relación con Tarjeta de Crédito
        public int? TarjetaCreditoId { get; set; }
        public ICollection<TarjetaCreditoViewModel> TarjetasCredito { get; set; }

        // Relación con Préstamo
        public int? PrestamoId { get; set; }
        public ICollection<PrestamoViewModel> Prestamos { get; set; }

        // Relación con Beneficiario
        public int? BeneficiarioId { get; set; }
        public ICollection<BeneficiarioViewModel> Beneficiarios { get; set; }
    }
}