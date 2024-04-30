

using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Transferencia;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class TransferenciaService : GenericService<SaveTransferenciaViewModel , TransferenciaViewModel , Transferencia> , ITransferenciaService
    {
        private readonly IMapper mapper;
        private readonly ITransferencia transferenciaRepository;
        private readonly ICuentaAhorro cuentaAhorroRepository;

        public TransferenciaService(IMapper mapper , ITransferencia transferenciaRepository, ICuentaAhorro cuentaAhorroRepository) : base(transferenciaRepository,mapper) 
        {
            this.mapper = mapper;
            this.transferenciaRepository = transferenciaRepository;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
        }

        public override async Task<SaveTransferenciaViewModel> Add(SaveTransferenciaViewModel vm)
        {

            var cuentaSalida = await cuentaAhorroRepository.GetByIdAsync((int)vm.IdCuentaAhorro!);
            var cuentaEntrante = await cuentaAhorroRepository.GetByIdAsync((int)vm.CuentaDestinoId!);

            if(cuentaSalida.NumeroCuenta != cuentaEntrante.NumeroCuenta) 
            {
                if(vm.Monto > cuentaSalida.Saldo)
                {
                    throw new InvalidOperationException("No se puede realizar la tranferencia , No tienes el monto ingresado.");
                }
                else
                {
                    cuentaSalida.Saldo -= vm.Monto;
                    await cuentaAhorroRepository.UpdateAsync(cuentaSalida, cuentaSalida.IdCuentaAhorro);
                    cuentaEntrante.Saldo += vm.Monto;
                    await cuentaAhorroRepository.UpdateAsync(cuentaEntrante, cuentaEntrante.IdCuentaAhorro);
                   
                }
            }
            else
            {
                throw new InvalidOperationException("No se puede realizar la tranferencia , las dos cuentas ingresadas son las misma.");
            }

            Transferencia entity = mapper.Map<Transferencia>(vm);

            entity = await transferenciaRepository.AddAsync(entity);

            SaveTransferenciaViewModel entityVm = mapper.Map<SaveTransferenciaViewModel>(entity);

            return entityVm;
        }
    }
}
