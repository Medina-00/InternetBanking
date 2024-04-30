

using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICuentaAhorroService : IGenericService<SaveCuentaAhorroViewModel, CuentaAhorroViewModel,CuentaAhorro>
    {
    }
}
