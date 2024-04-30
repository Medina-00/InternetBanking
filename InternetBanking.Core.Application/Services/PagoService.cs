using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Pago;
using InternetBanking.Core.Domain.Entities;


namespace InternetBanking.Core.Application.Services
{
    public class PagoService : GenericService<SavePagoViewModel, PagoViewModel, Pago>, IPagoService
    {
        private readonly IMapper mapper;
        private readonly IPago pagoRepository;
        private readonly ICuentaAhorro cuentaAhorroRepository;
        private readonly IPrestamo prestamoRepository;
        private readonly ITarjetaCredito tarjetaCreditoRepository;
        private readonly IBeneficiario beneficiarioRepository;

        public PagoService(IMapper mapper,  IPago pagoRepository, ICuentaAhorro cuentaAhorroRepository
            , IPrestamo prestamoRepository , ITarjetaCredito tarjetaCreditoRepository , IBeneficiario beneficiarioRepository) : base(pagoRepository, mapper)
        {
            this.mapper = mapper;
            this.pagoRepository = pagoRepository;
            this.cuentaAhorroRepository = cuentaAhorroRepository;
            this.prestamoRepository = prestamoRepository;
            this.tarjetaCreditoRepository = tarjetaCreditoRepository;
            this.beneficiarioRepository = beneficiarioRepository;
        }

        public override async Task<SavePagoViewModel> Add(SavePagoViewModel vm)
        {
            var cuentas = await cuentaAhorroRepository.GetAllAsync();
            var prestamos = await prestamoRepository.GetAllAsync();
            var tarjetas = await tarjetaCreditoRepository.GetAllAsync();
            var cuentaSalida = await cuentaAhorroRepository.GetByIdAsync((int)vm.CuentaAhorroId!);
            var Beneficiarios = await beneficiarioRepository.GetAllAsync();
            
            if (vm.TipoPago == "Expreso") 
            { 
               if (cuentaSalida!.Saldo > vm.Monto)
               {
                    cuentaSalida.Saldo -= vm.Monto;
                    await cuentaAhorroRepository.UpdateAsync(cuentaSalida, cuentaSalida.IdCuentaAhorro);
                    var cuentaentrante = cuentas.Find(c => c.NumeroCuenta == vm.NumeroCuenta);
                    if(cuentaentrante != null)
                    {
                        cuentaentrante!.Saldo += vm.Monto;
                        await cuentaAhorroRepository.UpdateAsync(cuentaentrante, cuentaentrante.IdCuentaAhorro);

                    }
                    else
                    {
                        throw new InvalidOperationException("La cuenta de destino no existe.");
                    }

               }
               else
               {
                    throw new InvalidOperationException("No se puede realizar el pago , no posees el monto suficiente en la cuenta.");

               }
            }
            else if(vm.TipoPago == "Prestamo")
            {
                var prestamoEncontrado = prestamos.Find(c => c.IdPrestamo == vm.PrestamoId);
                
                //verificamos si encotramos el prestamo
                if (prestamoEncontrado != null)
                {
                    //esto es para que si usuario esta pagando mas que la deuda , que solo se le descuente lo de la deuda
                    //verificamos si el monto a pagar el mayor o igual que la deuda del prestamo
                    if (prestamoEncontrado!.Deuda >= vm.Monto)
                    {
                        // El cliente está pagando la cantidad completa de la deuda
                        prestamoEncontrado.Deuda -= vm.Monto;
                    }
                    else
                    {
                        vm.Monto = prestamoEncontrado.Deuda;
                        prestamoEncontrado.Deuda = 0;
                    }

                    cuentaSalida.Saldo -= vm.Monto;
                    await cuentaAhorroRepository.UpdateAsync(cuentaSalida, cuentaSalida.IdCuentaAhorro);
                    await prestamoRepository.UpdateAsync(prestamoEncontrado, prestamoEncontrado.IdPrestamo);
                }
                else
                {
                    
                    throw new InvalidOperationException("El numero del prestamo de destino no existe.");
                }
            }
            else if (vm.TipoPago == "TarjetaCredito")
            {
                var TarjetaEncontrada = tarjetas.Find(c => c.IdTarjetaCredito == vm.TarjetaCreditoId);
                if (TarjetaEncontrada != null)
                {
                    //esto es para que si usuario esta pagando mas que la deuda , que solo se le descuente lo de la deuda
                    if (TarjetaEncontrada!.Deuda >= vm.Monto)
                    {
                        // El cliente está pagando la cantidad completa de la deuda
                        TarjetaEncontrada.Deuda -= vm.Monto;
                    }
                    else
                    {
                        vm.Monto = TarjetaEncontrada.Deuda;
                        TarjetaEncontrada.Deuda = 0;
                    }

                }
                else
                {

                    throw new InvalidOperationException("El numero de la tarjeta de credito de destino no existe.");
                }
                cuentaSalida.Saldo -= vm.Monto;
                await tarjetaCreditoRepository.UpdateAsync(TarjetaEncontrada, TarjetaEncontrada.IdTarjetaCredito);
                await cuentaAhorroRepository.UpdateAsync(cuentaSalida, cuentaSalida.IdCuentaAhorro);
            }
            else if (vm.TipoPago == "Beneficiario")
            {
                cuentaSalida.Saldo -= vm.Monto;
                await cuentaAhorroRepository.UpdateAsync(cuentaSalida, cuentaSalida.IdCuentaAhorro);
                var BeneficiarioEncontrado = Beneficiarios.Find(c => c.IdBeneficiario == vm.BeneficiarioId);
                var cuentaBeneficiario = cuentas.Find(c => c.NumeroCuenta == BeneficiarioEncontrado!.NumeroCuenta);
                if (BeneficiarioEncontrado != null || cuentaBeneficiario != null)
                {
                    cuentaBeneficiario!.Saldo += vm.Monto;
                    await cuentaAhorroRepository.UpdateAsync(cuentaBeneficiario, cuentaBeneficiario.IdCuentaAhorro);

                }
                else
                {

                    throw new InvalidOperationException("La cuenta del Beneficiario destino no existe.");
                }
            }


            Pago entity = mapper.Map<Pago>(vm);
            entity = await pagoRepository.AddAsync(entity);
            SavePagoViewModel entityVm = mapper.Map<SavePagoViewModel>(entity);

            return entityVm;
        }
    }
}