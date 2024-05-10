using Microsoft.AspNetCore.Mvc;
using SW.Web.Models;
using System.Diagnostics;

namespace SW.Web.Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("~/Index")]
        public IActionResult Index()
        {
            ViewData["Title"] = "P�gina Inicial";
            return View();
        }
        [HttpGet("~/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet(Name ="Duration")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
