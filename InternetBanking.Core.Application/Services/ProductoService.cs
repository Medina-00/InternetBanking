
using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Producto;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class ProductoService : GenericService<SaveProductoViewModel , ProductoViewModel , Producto> , IProductoService
    {
        private readonly IMapper mapper;
        private readonly IProducto productoRepository;

        public ProductoService(IMapper mapper, IProducto productoRepository) : base(productoRepository,mapper)
        {
            this.mapper = mapper;
            this.productoRepository = productoRepository;
        }
    }
}
