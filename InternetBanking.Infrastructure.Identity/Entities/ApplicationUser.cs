using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ?Nombre { get; set; }

        public string? Apellido { get; set; }

        public double? MontoInicial { get; set; }

        public bool? Activo { get; set; }



        public List<Producto> Productos { get; set; }

        public List<Beneficiario> Beneficiarios { get; set; }




    }
}
