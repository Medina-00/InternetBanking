

using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account.Request;
using InternetBanking.Core.Application.ViewModels.AvanceEfectivo;
using InternetBanking.Core.Application.ViewModels.Beneficiario;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.Pago;
using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Core.Application.ViewModels.Producto;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using InternetBanking.Core.Application.ViewModels.Transferencia;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UpdateUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<Producto, ProductoViewModel>()
                .ReverseMap();

            CreateMap<Producto, SaveProductoViewModel>()
               .ReverseMap()
               .ForMember(d => d.IDProducto, o => o.Ignore())
                 .ForMember(d => d.Numero9Digitos, o => o.Ignore());





            CreateMap<CuentaAhorro, CuentaAhorroViewModel>()

                .ReverseMap()
                .ForMember(d => d.Pagos, o => o.Ignore())
                .ForMember(d => d.AvanceEfectivos, o => o.Ignore());

            CreateMap<CuentaAhorro, SaveCuentaAhorroViewModel>()

               .ReverseMap()
                .ForMember(d => d.NumeroProducto, o => o.Ignore())
               .ForMember(d => d.IdCuentaAhorro, o => o.Ignore())
               .ForMember(d => d.NumeroCuenta, o => o.Ignore())
               .ForMember(d => d.Pagos, o => o.Ignore())
               .ForMember(d => d.AvanceEfectivos, o => o.Ignore());

            CreateMap<Prestamo, PrestamoViewModel>()

                .ReverseMap()
                .ForMember(d => d.Pagos, o => o.Ignore());

            CreateMap<Prestamo, SavePrestamoViewModel>()

                .ReverseMap()
                  .ForMember(d => d.NumeroProducto, o => o.Ignore())
                 .ForMember(d => d.IdPrestamo, o => o.Ignore())
                 .ForMember(d => d.Deuda, o => o.Ignore())
                .ForMember(d => d.NumeroPrestamo, o => o.Ignore())

                .ForMember(d => d.Pagos, o => o.Ignore());

            CreateMap<TarjetaCredito, TarjetaCreditoViewModel>()

                .ReverseMap()
                .ForMember(d => d.AvanceEfectivos, o => o.Ignore())
                .ForMember(d => d.Pagos, o => o.Ignore());

            CreateMap<TarjetaCredito, SaveTarjetaCreditoViewModel>()

               .ReverseMap()
               .ForMember(d => d.NumeroProducto, o => o.Ignore())
               .ForMember(d => d.IdTarjetaCredito, o => o.Ignore())
               .ForMember(d => d.NumeroTarjeta, o => o.Ignore())
               .ForMember(d => d.Deuda, o => o.Ignore())
               .ForMember(d => d.EsPrincipal, o => o.Ignore())
               .ForMember(d => d.AvanceEfectivos, o => o.Ignore())
               .ForMember(d => d.Pagos, o => o.Ignore());

            CreateMap<Beneficiario, BeneficiarioViewModel>()
                .ReverseMap();

            CreateMap<Beneficiario, SaveBeneficiarioViewModel>()

                .ReverseMap()
                .ForMember(d => d.IdBeneficiario, o => o.Ignore());

            CreateMap<Pago, PagoViewModel>()
                .ReverseMap();

            CreateMap<Pago, SavePagoViewModel>()
                .ForMember(d => d.TipoPago, o => o.Ignore())
               .ReverseMap()
               .ForMember(d => d.IdPago, o => o.Ignore());

            CreateMap<AvanceEfectivo, AvanceEfectivoViewModel>()
               .ReverseMap()
               .ForMember(d => d.CuentaAhorro, o => o.Ignore())
              .ForMember(d => d.TarjetaCredito, o => o.Ignore());

           


            CreateMap<AvanceEfectivo, SaveAvanceEfectivoViewModel>()
                .ForMember(d => d.CuentaAhorro, o => o.Ignore())
               .ForMember(d => d.TarjetaCredito, o => o.Ignore())
               .ReverseMap()
               .ForMember(d => d.CuentaAhorro, o => o.Ignore())
               .ForMember(d => d.TarjetaCredito, o => o.Ignore())
               .ForMember(d => d.IdAvanceEfectivo, o => o.Ignore());


            CreateMap<Transferencia, TransferenciaViewModel>()
               .ReverseMap()
               .ForMember(d => d.Monto, o => o.Ignore())
               .ForMember(d => d.IdCuentaAhorro, o => o.Ignore())
               .ForMember(d => d.CuentaDestinoId, o => o.Ignore())
                .ForMember(d => d.CuentaDestino, o => o.Ignore())
               .ForMember(d => d.CuentaOrigen, o => o.Ignore());


            CreateMap<Transferencia, SaveTransferenciaViewModel>()
                .ForMember(d => d.CuentaOrigen, o => o.Ignore())
               .ForMember(d => d.CuentaDestino, o => o.Ignore())
               .ReverseMap()
               .ForMember(d => d.IdTransferencia, o => o.Ignore())
               .ForMember(d => d.fechaPago, o => o.Ignore())
               .ForMember(d => d.CuentaOrigen, o => o.Ignore())
               .ForMember(d => d.CuentaDestino, o => o.Ignore());


        }
    }
}
