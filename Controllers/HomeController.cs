using System.Diagnostics;
using ETR_IPT3D_TEAM3.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETR_IPT3D_TEAM3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult About() => View();
        public IActionResult Contact() => View();
        public IActionResult FAQs() => View();

    }
}
