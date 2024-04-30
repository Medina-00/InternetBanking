using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    public class PrestamoService : GenericService<SavePrestamoViewModel, PrestamoViewModel, Prestamo>, IPrestamoService
    {
        private readonly IMapper mapper;
        private readonly IPrestamo prestamoRepository;
        private readonly ICuentaAhorro cuentaAhorroRepository;
        private readonly IProducto productoRepository;

        public PrestamoService(IMapper mapper, IPrestamo prestamoRepository,ICuentaAhorro cuentaAhorroRepository, IProducto productoRepository) : base(prestamoRepository, mapper)
        {
             this.mapper = mapper;
             this.prestamoRepository = prestamoRepository;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
            this.productoRepository = productoRepository;
        }

        public override async Task<SavePrestamoViewModel> Add(SavePrestamoViewModel vm)
        {
            var productos = await productoRepository.GetAllAsync();
            var Pprestamo = productos.Find(p => p.UserId == vm.UserId && p.Tipo == TipoProducto.Prestamo.ToString());
            Prestamo entity = mapper.Map<Prestamo>(vm);
            entity.NumeroProducto = Pprestamo!.Numero9Digitos;
            entity = await prestamoRepository.AddAsync(entity);

            SavePrestamoViewModel entityVm = mapper.Map<SavePrestamoViewModel>(entity);

            var cuentas = await cuentaAhorroRepository.GetAllAsync();

            var cuentaAhorro = cuentas.Find(c => c.UserId == vm.UserId);

            cuentaAhorro!.Saldo += vm.Monto;
            await cuentaAhorroRepository.UpdateAsync(cuentaAhorro,cuentaAhorro.IdCuentaAhorro);

            return entityVm;
        }

        public override async Task Delete(int id)
        {
            var product = await prestamoRepository.GetByIdAsync(id);
            if(product.Deuda != 0) 
            {
                throw new InvalidOperationException("No se puede borrar el préstamo porque tiene una deuda pendiente.");
            }
            await prestamoRepository.DeleteAsync(product);
        }

    }
}
