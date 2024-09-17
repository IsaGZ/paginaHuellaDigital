using DPUruNet;
using huellaDigital_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using TeracromControllers;
using TeracromModels;

namespace huellaDigital_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LogInController log;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            log = new LogInController();
        }

        public IActionResult Index()
        {
            return View();
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
        [HttpGet]
        public async Task<Dictionary<int, Fmd>> getHuella()
        {
            Dictionary<int, Fmd> f = await log.obtenerUsuariosHuella();

            return f;
        }
    }
}
