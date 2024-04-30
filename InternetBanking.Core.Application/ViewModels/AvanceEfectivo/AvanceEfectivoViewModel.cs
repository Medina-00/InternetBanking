namespace InternetBanking.Core.Application.ViewModels.AvanceEfectivo
{
    public class AvanceEfectivoViewModel
    {
        public int IdAvanceEfectivo { get; set; }
        public decimal Monto { get; set; }
        public decimal Interes { get; set; }

        public DateTime fechaPago { get; set; } = DateTime.Now;


        // Relación con Tarjeta de Crédito
        public int IdTarjetaCredito { get; set; }
      

        // Relación con Cuenta de Ahorro
        public int IdCuentaAhorro { get; set; }
    }

}

