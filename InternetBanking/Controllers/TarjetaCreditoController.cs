using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class TarjetaCreditoController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITarjetaCreditoService tarjetaCreditoService;

        public TarjetaCreditoController(UserManager<ApplicationUser> userManager , ITarjetaCreditoService tarjetaCreditoService)
        {
            this.userManager = userManager;
            this.tarjetaCreditoService = tarjetaCreditoService;
        }


        // GET: TarjetaCreditoController/Create
        public async Task<ActionResult> Create()
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            return View(new SaveTarjetaCreditoViewModel { UserId = currentUser!.Id});
        }

        // POST: TarjetaCreditoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveTarjetaCreditoViewModel saveTarjeta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveTarjeta);
                }

                await tarjetaCreditoService.Add(saveTarjeta);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch
            {
                return View();
            }
        }


        // GET: TarjetaCreditoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await tarjetaCreditoService.GetByIdSaveViewModel(id);
            return View();
        }

        // POST: TarjetaCreditoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveTarjetaCreditoViewModel saveTarjeta)
        {
            try
            {
                await tarjetaCreditoService.Delete(id);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorTarjeta");
            }
        }
    }
}
