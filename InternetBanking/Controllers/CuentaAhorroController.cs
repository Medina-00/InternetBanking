using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.CuentaAhorro;
using InternetBanking.Core.Application.ViewModels.TarjetaCredito;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class CuentaAhorroController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICuentaAhorroService cuentaAhorroService;

        public CuentaAhorroController(UserManager<ApplicationUser> userManager, ICuentaAhorroService cuentaAhorroService)
        {
            this.userManager = userManager;
            this.cuentaAhorroService = cuentaAhorroService;
        }
        // GET: CuentaAhorroController/Create
        public async Task<ActionResult> Create()
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            return View(new SaveCuentaAhorroViewModel { UserId = currentUser!.Id });
        }

        // POST: CuentaAhorroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveCuentaAhorroViewModel cuentaAhorroViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(cuentaAhorroViewModel);
                }

                await cuentaAhorroService.Add(cuentaAhorroViewModel);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorCuenta");
            }

        }

        

        // GET: CuentaAhorroController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await cuentaAhorroService.GetByIdSaveViewModel(id);
            return View(data);
        }

        // POST: CuentaAhorroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveCuentaAhorroViewModel saveCuenta)
        {
            try
            {
                await cuentaAhorroService.Delete(id);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorCuenta");
            }
        }
    }
}
