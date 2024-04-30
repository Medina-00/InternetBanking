using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Prestamo;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class PrestamoController : Controller
    {
        private readonly IPrestamoService prestamoService;
        private readonly UserManager<ApplicationUser> userManager;

        public PrestamoController(IPrestamoService prestamoService , UserManager<ApplicationUser> userManager)
        {
            this.prestamoService = prestamoService;
            this.userManager = userManager;
        }


        // GET: PrestamoController/Create
        public ActionResult Create()
        {
            return View(new SavePrestamoViewModel());
        }

        // POST: PrestamoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SavePrestamoViewModel savePrestamo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(savePrestamo);
                }
                //Obtener el usuario actualmente autenticado
                var currentUser = await userManager.GetUserAsync(User);
                savePrestamo.UserId = currentUser!.Id;
                await prestamoService.Add(savePrestamo);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch
            {
                return View();
            }
        }

        

        // GET: PrestamoController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await prestamoService.GetByIdSaveViewModel(id);

            return View(data);
        }

        // POST: PrestamoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SavePrestamoViewModel prestamoViewModel)
        {
            try
            {
                await prestamoService.Delete(id);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorPrestamo");
            }
        }
    }
}
