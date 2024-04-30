using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Domain.Entities
{
    public class Producto
    {
        [Key]

        public int IDProducto { get; set; }
        public string UserId { get; set; }
        public string Tipo { get; set; }
        public string Numero9Digitos { get; set; }
       
    }
}
