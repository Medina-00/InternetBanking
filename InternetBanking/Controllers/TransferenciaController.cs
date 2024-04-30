using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Transferencia;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    public class TransferenciaController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITransferenciaService transferenciaService;
        private readonly ICuentaAhorroService cuentaAhorroService;

        public TransferenciaController(UserManager<ApplicationUser> userManager, ITransferenciaService transferenciaService
            ,ICuentaAhorroService cuentaAhorroService)
        {
            this.userManager = userManager;
            this.transferenciaService = transferenciaService;
            this.cuentaAhorroService = cuentaAhorroService;
        }
        
        public async Task<ActionResult> Create()
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            var cuenta = await cuentaAhorroService.GetAllViewModel();
            SaveTransferenciaViewModel saveTransferencia = new();
            saveTransferencia.CuentaOrigen = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            saveTransferencia.CuentaDestino = cuenta.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(saveTransferencia);
        }

        // POST: TransferenciaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveTransferenciaViewModel saveTransferencia)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveTransferencia);
                }
                await transferenciaService.Add(saveTransferencia);
                return RedirectToRoute(new { controller = "Producto", action = "Index" });
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("ErrorTranferencia");
            }

        }

        
    }
}
