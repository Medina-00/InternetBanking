

using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiario;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiarioService : GenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario>, IBeneficiarioService
    {
        private readonly IMapper mapper;
        private readonly IBeneficiario beneficiarioRepository;

        public BeneficiarioService(IMapper mapper,IBeneficiario beneficiarioRepository) : base(beneficiarioRepository, mapper)
        {
            this.mapper = mapper;
            this.beneficiarioRepository = beneficiarioRepository;
        }

    }
}