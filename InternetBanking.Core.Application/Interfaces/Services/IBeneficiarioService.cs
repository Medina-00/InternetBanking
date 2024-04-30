

using InternetBanking.Core.Application.ViewModels.Beneficiario;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiarioService : IGenericService<SaveBeneficiarioViewModel,BeneficiarioViewModel,Beneficiario>
    {
    }
}
