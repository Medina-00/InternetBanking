
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infrastructure.Persistence.Context;
using InternetBanking.Infrastructure.Persistence.Reposiroty;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetBanking.Infrastructure.Persistence.Repository
{
    public class TransferenciaRepository : GenericRepository<Transferencia>, ITransferencia
    {
        private readonly ApplicationContext applicationContext;

        public TransferenciaRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        public override async Task<List<Transferencia>> GetAllAsync()
        {
            return await applicationContext.Transferencias
               .Select(t => new Transferencia
               {
                   IdTransferencia = t.IdTransferencia,
                   fechaPago = t.fechaPago
               })
               .ToListAsync();
       
        }
    }
}
