using Microsoft.AspNetCore.Mvc;
using SW.Web.Models;
using System.Diagnostics;

namespace SW.Web.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("[controller]/Index")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Página Inicial";
            return View();
        }
        [HttpGet("[controller]/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("[controller]/Logged")]
        public IActionResult Logged()
        {
            return View();
        }
    }
}
