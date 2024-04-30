using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiario;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class BeneficiarioController : Controller
    {
        private readonly IBeneficiarioService beneficiarioService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;

        public BeneficiarioController(IBeneficiarioService beneficiarioService 
            , UserManager<ApplicationUser> userManager
            ,IUserService userService)
        {
            this.beneficiarioService = beneficiarioService;
            this.userManager = userManager;
            this.userService = userService;
        }
        // GET: BeneficiarioController
        public async Task<ActionResult> Index()
        {
            ViewBag.Users = await userService.GetAllUser();
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            var data = await beneficiarioService.GetAllViewModel();
            return View(data.Where(d => d.UserId == currentUser!.Id));
        }

        
        // GET: BeneficiarioController/Create
        public async Task<ActionResult> Create()
        {
            //Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            return View(new SaveBeneficiarioViewModel { UserId = currentUser!.Id});
        }

        // POST: BeneficiarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveBeneficiarioViewModel saveBeneficiario)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(saveBeneficiario);
                }
                await beneficiarioService.Add(saveBeneficiario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

        // GET: BeneficiarioController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var data = await beneficiarioService.GetByIdSaveViewModel(id);
            return View(data);
        }

        // POST: BeneficiarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, SaveBeneficiarioViewModel saveBeneficiario)
        {
            try
            {
                await beneficiarioService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
