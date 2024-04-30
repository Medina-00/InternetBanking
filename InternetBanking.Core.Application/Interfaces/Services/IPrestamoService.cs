

using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPrestamoService : IGenericService<SavePrestamoViewModel,PrestamoViewModel,Prestamo>
    {
    }
}
