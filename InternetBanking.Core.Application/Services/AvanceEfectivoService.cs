

using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.AvanceEfectivo;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class AvanceEfectivoService : GenericService<SaveAvanceEfectivoViewModel,  AvanceEfectivoViewModel ,AvanceEfectivo> , IAvenceEfectivoService
    {
        private readonly IMapper mapper;
        private readonly IAvenceEfectivo avenceEfectivoRepository;
        private readonly ITarjetaCredito tarjetaCreditoRepository;
        private readonly ICuentaAhorro cuentaAhorroRepository;

        public AvanceEfectivoService(IMapper mapper,IAvenceEfectivo avenceEfectivoRepository ,
            ITarjetaCredito tarjetaCreditoRepository , ICuentaAhorro cuentaAhorroRepository) : base(avenceEfectivoRepository,mapper)
        {
            this.mapper = mapper;
            this.avenceEfectivoRepository = avenceEfectivoRepository;
            this.tarjetaCreditoRepository = tarjetaCreditoRepository;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
        }

        public override async Task<SaveAvanceEfectivoViewModel> Add(SaveAvanceEfectivoViewModel vm)
        {
            var tarjetas = await tarjetaCreditoRepository.GetAllAsync();
            var cuentas = await cuentaAhorroRepository.GetAllAsync();
            var cuentaentrante = cuentas.Find(c => c.IdCuentaAhorro == vm.IdCuentaAhorro);
            var TarjetaEncontrada = tarjetas.Find(c => c.IdTarjetaCredito == vm.IdTarjetaCredito);
            const decimal valorInteres = (decimal)0.0625;
            if (vm.Monto > TarjetaEncontrada!.LimiteCredito)
            {
                
                throw new InvalidOperationException("No se Pudo realizar El Avance Estas exediendo el limete de la tarjeta.");
            }
            else
            {
                cuentaentrante!.Saldo += vm.Monto;
                await cuentaAhorroRepository.UpdateAsync(cuentaentrante, cuentaentrante.IdCuentaAhorro);
                vm.Interes = vm.Monto * valorInteres;
                TarjetaEncontrada.Deuda += vm.Monto + vm.Interes;
                await tarjetaCreditoRepository.UpdateAsync(TarjetaEncontrada, TarjetaEncontrada.IdTarjetaCredito);
            }

            AvanceEfectivo entity = mapper.Map<AvanceEfectivo>(vm);
            entity = await avenceEfectivoRepository.AddAsync(entity);
            SaveAvanceEfectivoViewModel entityVm = mapper.Map<SaveAvanceEfectivoViewModel>(entity);

            return entityVm;
        }
    }
}
