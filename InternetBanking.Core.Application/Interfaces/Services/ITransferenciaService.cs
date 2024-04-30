
using InternetBanking.Core.Application.ViewModels.Transferencia;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITransferenciaService : IGenericService<SaveTransferenciaViewModel,TransferenciaViewModel, Transferencia>
    {
    }
}
