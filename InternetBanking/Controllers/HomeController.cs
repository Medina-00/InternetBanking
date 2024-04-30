using InternetBanking.Core.Application.Interfaces.Repository;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Home;
using InternetBanking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InternetBanking.Controllers
{
    [Authorize(Roles = "Administrador")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductoService productoService;
        private readonly IPagoService pagoService;
        private readonly IUserService userService;
        private readonly IAvenceEfectivoService avenceEfectivoService;
        private readonly ITransferenciaService transferenciaService;

        public HomeController(ILogger<HomeController> logger
            , IProductoService productoService, IPagoService pagoService,
            IUserService userService, IAvenceEfectivoService avenceEfectivoService
            ,ITransferenciaService transferenciaService)
        {
            _logger = logger;
            this.productoService = productoService;
            this.pagoService = pagoService;
            this.userService = userService;
            this.avenceEfectivoService = avenceEfectivoService;
            this.transferenciaService = transferenciaService;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Today;
            DashboardViewModel dashboard = new();
            dashboard.Productos = (await productoService.GetAllViewModel()).Count();
            dashboard.UsuarioInactivo = (await userService.GetAllUser()).Where(u => u.Activo == false).ToList().Count();
            dashboard.UsuarioActivo = (await userService.GetAllUser()).Where(u => u.Activo == true).ToList().Count();
            dashboard.PagosTotal = (await pagoService.GetAllViewModel()).Count();
            // Filtrar los pagos por la fecha de creación igual a la fecha actual
            dashboard.PagosDia = (await pagoService.GetAllViewModel())
                .Where(p => p.fechaPago.Date == fechaActual)
                .Count();

            var avancesdia = (await avenceEfectivoService.GetAllViewModel())
                .Where(p => p.fechaPago.Date == fechaActual)
                .Count();

            var transferenciadia = (await transferenciaService.GetAllViewModel())
                .Where(p => p.fechaPago.Date == fechaActual)
                .Count();

            var avancestotal = (await avenceEfectivoService.GetAllViewModel()).Count();

            var transferenciatotal = (await transferenciaService.GetAllViewModel()).Count();
            dashboard.TransaccionesDia = dashboard.PagosDia + avancesdia + transferenciadia;
            dashboard.TransaccionesTotal = dashboard.PagosTotal + avancestotal + transferenciatotal; 
            return View(dashboard);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
