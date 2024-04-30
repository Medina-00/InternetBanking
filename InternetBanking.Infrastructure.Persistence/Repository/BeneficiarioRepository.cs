using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using System;


namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class BeneficiarioRepository : GenericRepository<Beneficiario>, IBeneficiario
    {
        private readonly ApplicationContext applicationContext;

        public BeneficiarioRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public override async Task<Beneficiario> AddAsync(Beneficiario t)
        {
            await applicationContext.Set<Beneficiario>().AddAsync(t);
             var cuenta = applicationContext.CuentasAhorro.Where(c => c.NumeroCuenta ==  t.NumeroCuenta).FirstOrDefault();
            t.UserIdBeneficiario = cuenta!.UserId;
            await applicationContext.SaveChangesAsync();
            return t;
        }
    }
}
