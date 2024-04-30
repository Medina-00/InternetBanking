using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    public class TarjetaCreditoService : GenericService<SaveTarjetaCreditoViewModel, TarjetaCreditoViewModel, TarjetaCredito>, ITarjetaCreditoService
    {
        private readonly IMapper mapper;
        private readonly ITarjetaCredito tarjetaCreditoRepository;
        private readonly IProducto productoRepository;

        public TarjetaCreditoService(IMapper mapper, ITarjetaCredito tarjetaCreditoRepository, IProducto productoRepository) : base(tarjetaCreditoRepository, mapper)
        {
            this.mapper = mapper;
            this.tarjetaCreditoRepository = tarjetaCreditoRepository;
            this.productoRepository = productoRepository;
        }

        public override async Task<SaveTarjetaCreditoViewModel> Add(SaveTarjetaCreditoViewModel vm)
        {
            var productos = await productoRepository.GetAllAsync();
            var Ptarjeta = productos.Find(p => p.UserId == vm.UserId && p.Tipo == TipoProducto.TarjetaCredito.ToString());
            TarjetaCredito entity = mapper.Map<TarjetaCredito>(vm);
            entity.NumeroProducto = Ptarjeta!.Numero9Digitos;
            entity = await tarjetaCreditoRepository.AddAsync(entity);

            SaveTarjetaCreditoViewModel entityVm = mapper.Map<SaveTarjetaCreditoViewModel>(entity);

            return entityVm;
        }
        public override async Task Delete(int id)
        {
            var tarjeta = await tarjetaCreditoRepository.GetByIdAsync(id);
            if (tarjeta.Deuda != 0)
            {
                throw new InvalidOperationException("No se puede borrar la Tarjeta porque tiene una deuda pendiente.");
            }
            await tarjetaCreditoRepository.DeleteAsync(tarjeta);
        }
    }
    
}
