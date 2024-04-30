using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    public class CuentaAhorroService : GenericService<SaveCuentaAhorroViewModel, CuentaAhorroViewModel, CuentaAhorro>, ICuentaAhorroService
    {
        private readonly IMapper mapper;
        private readonly ICuentaAhorro cuentaAhorroRepository;
        private readonly IProducto productoRepository;

        public CuentaAhorroService(IMapper mapper, ICuentaAhorro cuentaAhorroRepository , IProducto productoRepository ) : base(cuentaAhorroRepository, mapper)
        {
            this.mapper = mapper;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
            this.productoRepository = productoRepository;
        }

        public override async Task<SaveCuentaAhorroViewModel> Add(SaveCuentaAhorroViewModel vm)
        {
            var productos = await productoRepository.GetAllAsync();
            var cuentas = await cuentaAhorroRepository.GetAllAsync();
            CuentaAhorro entity = mapper.Map<CuentaAhorro>(vm);
            var PCuentaAhorro = productos.Find(p => p.UserId == vm.UserId && p.Tipo == TipoProducto.CuentaAhorro.ToString());
            entity.NumeroProducto = PCuentaAhorro!.Numero9Digitos;
            if(cuentas.Any(c => c.EsPrincipal ==true && vm.EsPrincipal == true && c.UserId == vm.UserId))
            {
                throw new InvalidOperationException("No se puede borrar la agregar la cuenta ya que tiene una cuenta de Ahorro Principal.");
            }
     
            entity = await cuentaAhorroRepository.AddAsync(entity);

            SaveCuentaAhorroViewModel entityVm = mapper.Map<SaveCuentaAhorroViewModel>(entity);

            return entityVm;
        }

        public override async Task Delete(int id)
        {
            var cuentas = await cuentaAhorroRepository.GetAllAsync();
            var cuenta = await cuentaAhorroRepository.GetByIdAsync(id);
            var otraCuenta = cuentas.Find(c => c.NumeroCuenta != cuenta.NumeroCuenta);
            if(cuenta.EsPrincipal == true)
            {
                throw new InvalidOperationException("No se puede borrar la cuenta de Ahorro Principal.");

            }

            otraCuenta!.Saldo += cuenta.Saldo;
            await cuentaAhorroRepository.UpdateAsync(otraCuenta, otraCuenta.IdCuentaAhorro);

            
            
            await cuentaAhorroRepository.DeleteAsync(cuenta);
        }
    }
}
