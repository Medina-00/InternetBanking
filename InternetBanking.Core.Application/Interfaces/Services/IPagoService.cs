

using InternetBanking.Core.Application.ViewModels.Pago;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPagoService : IGenericService<SavePagoViewModel , PagoViewModel,Pago>
    {
    }
}
