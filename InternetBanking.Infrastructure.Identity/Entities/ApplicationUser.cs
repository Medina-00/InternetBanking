using Microsoft.AspNetCore.Identity;


namespace InternetBanking.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string ?Nombre { get; set; }

        public string? Apellido { get; set; }





    }
}
