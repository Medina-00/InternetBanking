using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Producto;
using InternetBanking.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Cliente")]

    public class ProductoController : Controller
    {
        private readonly IProductoService productoService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICuentaAhorroService cuentaAhorroService;
        private readonly IPrestamoService prestamoService;
        private readonly ITarjetaCreditoService tarjetaCreditoService;

        public ProductoController(IProductoService productoService, 
            UserManager<ApplicationUser> userManager,
            ICuentaAhorroService cuentaAhorroService,
            IPrestamoService prestamoService,
            ITarjetaCreditoService tarjetaCreditoService)
        {
            this.productoService = productoService;
            this.userManager = userManager;
            this.cuentaAhorroService = cuentaAhorroService;
            this.prestamoService = prestamoService;
            this.tarjetaCreditoService = tarjetaCreditoService;
        }
        // GET: ProductoController
        public async Task<ActionResult> Index()
        {
            // Obtener el usuario actualmente autenticado
            var currentUser = await userManager.GetUserAsync(User);
            var data = await productoService.GetAllViewModel();
            var cuentas = await cuentaAhorroService.GetAllViewModel();
            var Prestamos = await prestamoService.GetAllViewModel();
            var Tarjetas = await tarjetaCreditoService.GetAllViewModel();
            ViewBag.CuentaAhorro = cuentas.Where(c => c.UserId == currentUser!.Id).ToList();
            ViewBag.Prestamos = Prestamos.Where(c => c.UserId == currentUser!.Id).ToList();
            ViewBag.Tarjetas = Tarjetas.Where(c => c.UserId == currentUser!.Id).ToList();
            return View(data.Where(d => d.UserId == currentUser!.Id));
        }

       

        // GET: ProductoController/Create
        public ActionResult Create()
        {
            return View(new SaveProductoViewModel());
        }

        // POST: ProductoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SaveProductoViewModel saveProducto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(saveProducto);
                }

                // Obtener el usuario actualmente autenticado
                var currentUser = await userManager.GetUserAsync(User);
                if (saveProducto.Tipo == "Prestamo")
                {
                    saveProducto.UserId = currentUser!.Id;
                    await productoService.Add(saveProducto);
                    return RedirectToRoute(new { controller = "Prestamo", action = "Create" });

                }
                else if(saveProducto.Tipo == "TarjetaCredito")
                {
                    saveProducto.UserId = currentUser!.Id;
                    await productoService.Add(saveProducto);
                    return RedirectToRoute(new { controller = "TarjetaCredito", action = "Create" });
                }
                else if (saveProducto.Tipo == "CuentaAhorro")
                {
                    saveProducto.UserId = currentUser!.Id;
                    await productoService.Add(saveProducto);
                    return RedirectToRoute(new { controller = "CuentaAhorro", action = "Create" });
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
