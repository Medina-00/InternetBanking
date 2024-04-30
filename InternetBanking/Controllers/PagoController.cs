using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Pago;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class PagoController : Controller
    {
        private readonly IPagoService pagoService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICuentaAhorroService cuentaAhorroService;
        private readonly IPrestamoService prestamoService;
        private readonly ITarjetaCreditoService tarjetaCreditoService;
        private readonly IBeneficiarioService beneficiarioService;

        public PagoController(IPagoService pagoService , UserManager<ApplicationUser> userManager
            , ICuentaAhorroService cuentaAhorroService , IPrestamoService prestamoService
            ,ITarjetaCreditoService tarjetaCreditoService , IBeneficiarioService beneficiarioService)
        {
            this.pagoService = pagoService;
            this.userManager = userManager;
            this.cuentaAhorroService = cuentaAhorroService;
            this.prestamoService = prestamoService;
            this.tarjetaCreditoService = tarjetaCreditoService;
            this.beneficiarioService = beneficiarioService;
        }
        
        // GET: PagoController/Create
        public async Task<ActionResult> CreatePagoExpreso(string tipo)
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            SavePagoViewModel savePago = new();
            savePago.TipoPago = tipo;
            savePago.CuentasAhorro = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(savePago);
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePagoExpreso(SavePagoViewModel savePago)
        {
            try
            {
                
                await pagoService.Add(savePago);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorPago");
            }
        }

        // GET: PagoController/Create
        public async Task<ActionResult> CreatePagoTarjetaCredito(string tipo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            var tarjetas = await tarjetaCreditoService.GetAllViewModel();
            SavePagoViewModel savePago = new();
            savePago.TipoPago = tipo;
            savePago.CuentasAhorro = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            savePago.TarjetasCredito = tarjetas.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(savePago);
            
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePagoTarjetaCredito(SavePagoViewModel savePagoView)
        {
            try
            {
                
                await pagoService.Add(savePagoView);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorPago");
            }
        }
        // GET: PagoController/Create
        public async Task<ActionResult> CreatePagoPrestamo(string tipo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            var prestamos = await prestamoService.GetAllViewModel();
            SavePagoViewModel savePago = new();
            savePago.TipoPago = tipo;
            savePago.CuentasAhorro = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            savePago.Prestamos = prestamos.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(savePago);
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePagoPrestamo(SavePagoViewModel savePagoView)
        {
            try
            {
                
                await pagoService.Add(savePagoView);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorPago");
            }
        }
       
        public async Task<ActionResult> CreatePagoBeneficiario(string tipo)
        {
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            var beneficiarios = await beneficiarioService.GetAllViewModel();
            SavePagoViewModel savePago = new();
            savePago.TipoPago = tipo;
            savePago.CuentasAhorro = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            savePago.Beneficiarios = beneficiarios.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(savePago);
        }

        // POST: PagoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePagoBeneficiario(SavePagoViewModel savePagoView)
        {
            try
            {
                
                await pagoService.Add(savePagoView);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorPago");
            }
        }

        
    }
}
