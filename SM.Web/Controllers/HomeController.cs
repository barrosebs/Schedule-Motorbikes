using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Domain.Models;
using SW.Web.Models;
using System.Diagnostics;

namespace SW.Web.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserModel> _userManager;
        public HomeController(ILogger<HomeController> logger, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
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
            var Claims = _httpContextAccessor.HttpContext.User;
            var userLogado = _userManager.GetUserAsync(Claims).GetAwaiter().GetResult();
            return View(userLogado.HasAllocation);
        }
    }
}
