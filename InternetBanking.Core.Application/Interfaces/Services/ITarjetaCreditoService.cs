

using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITarjetaCreditoService : IGenericService<SaveTarjetaCreditoViewModel,TarjetaCreditoViewModel,TarjetaCredito>
    {
    }
}
