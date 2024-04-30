

using InternetBanking.Core.Application.ViewModels.Producto;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IProductoService : IGenericService<SaveProductoViewModel, ProductoViewModel, Producto>
    {
    }
}
