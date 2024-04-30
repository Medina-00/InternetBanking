

using InternetBanking.Core.Application.ViewModels.AvanceEfectivo;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IAvenceEfectivoService : IGenericService<SaveAvanceEfectivoViewModel , AvanceEfectivoViewModel, AvanceEfectivo>
    {
    }
}
