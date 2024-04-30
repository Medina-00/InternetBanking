

using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class AvanceEfectivoRepository : GenericRepository<AvanceEfectivo> , IAvenceEfectivo
    {
        public AvanceEfectivoRepository(ApplicationContext applicationContext) : base(applicationContext) { }
        
    }
}
