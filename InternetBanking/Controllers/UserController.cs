using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.Services;

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
        // GET: UserController
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
            if (authentication != null && authentication.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", authentication);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                loginView.HasError = authentication!.HasError;
                loginView.Error = authentication.Error;
                return View(loginView);
            }


        }

        [Authorize]
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
                return RedirectToRoute(new { controller = "Home", action = "Index" });
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
    }
}
