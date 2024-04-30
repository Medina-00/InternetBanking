

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class Beneficiario
    {
        [Key]

        public int IdBeneficiario { get; set; }
       
        public string NumeroCuenta { get; set; }
        public string UserId { get; set; }

        public string UserIdBeneficiario { get; set; }


    }
}
