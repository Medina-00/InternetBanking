using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.AvanceEfectivo;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.Pago;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]
    public class AvancesEfectivoController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICuentaAhorroService cuentaAhorroService;
        private readonly ITarjetaCreditoService tarjetaCreditoService;
        private readonly IAvenceEfectivoService avenceEfectivoService;

        public AvancesEfectivoController(UserManager<ApplicationUser> userManager 
            , ICuentaAhorroService cuentaAhorroService, ITarjetaCreditoService tarjetaCreditoService, IAvenceEfectivoService avenceEfectivoService)
        {
            this.userManager = userManager;
            this.cuentaAhorroService = cuentaAhorroService;
            this.tarjetaCreditoService = tarjetaCreditoService;
            this.avenceEfectivoService = avenceEfectivoService;
        }

        public async Task<ActionResult> Create()
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            var tarjetas = await tarjetaCreditoService.GetAllViewModel();
            SaveAvanceEfectivoViewModel saveAvance = new();
            saveAvance.TarjetaCredito = tarjetas.Where(c => c.UserId == currentUser!.Id).ToList();
            saveAvance.CuentaAhorro = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            saveAvance.Interes = 0;
            return View(saveAvance);
        }

        // POST: AvancesEfectivoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveAvanceEfectivoViewModel saveAvance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveAvance);
                }

                await avenceEfectivoService.Add(saveAvance);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorAvance");
            }
        }

        
    }
}
