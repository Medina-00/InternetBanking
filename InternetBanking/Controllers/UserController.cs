using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginView)
        {
           

            if (!ModelState.IsValid)
            {
                return View(loginView);
            }

            AuthenticationResponse authentication = await userService.LoginAsync(loginView);
            if(authentication.Activo != false)
            {

                if (authentication != null && authentication.HasError != true)
                {
                    HttpContext.Session.Set<AuthenticationResponse>("user", authentication);

                    if (authentication.Roles.Contains("Administrador"))
                    {
                        return RedirectToRoute(new { controller = "Home", action = "Index" });

                    }
                    else if(authentication.Roles.Contains("Cliente"))
                    {
                        return RedirectToRoute(new { controller = "Producto", action = "Index" });
                    }
                }
            
                    loginView.HasError = authentication!.HasError;
                    loginView.Error = authentication.Error;
                    return View(loginView);
            }
            else
            {
                await LogOut();
                ViewBag.ErrorMessage = "No Puede Acceder Su Usuario Esta Inactivo, comuniquese con el adminitrador para que lo active.";
                return View("ErrorUser");
            }
            


        }
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index()
        {
            // Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);

            var data = await userService.GetAllUser();
            return View(data.Where(u => u.UserId != currentUser!.Id));
        }

        

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View(new SaveUserViewModel());
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveUserViewModel saveUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveUser);
                }

                RegisterResponse request = await userService.RegisterAsync(saveUser);
                if (request.HasError)
                {
                    saveUser.HasError = request!.HasError;
                    saveUser.Error = request.Error;
                    return View(saveUser);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var data = await userService.GetByUserId(id);
            return View(data);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, UpdateUserViewModel saveUser)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveUser);
                }

                UpdateResponse request = await userService.UpdateUserAsync(id, saveUser);
                if (request.HasError)
                {
                    saveUser.HasError = request!.HasError;
                    saveUser.Error = request.Error;
                    return View(saveUser);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var data = await userService.GetByUserId(id);
            return View(data);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, UpdateUserViewModel collection)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                await userManager.DeleteAsync(user!);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public  ActionResult Activar(string id,string desicion)
        {
            return View(new ActivarUser { Activo = desicion});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Activar(string id, ActivarUser activarUser)
        {
            try
            {
                
                await userService.Activar(id, activarUser);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
