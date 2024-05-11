using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Domain.Models;
using SM.Domain.ViewModel;
using SM.Application.Extensions;

namespace SM.Web.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        IConfiguration _configuration;

        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IConfiguration configuration,UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, RoleManager<IdentityRole<int>> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("[controller]/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("[controller]/Login")]
        public IActionResult Login()
        {
            @ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost("[controller]/Login")]
        public async Task<IActionResult> Login([FromForm] LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.User);
                if (user == null)
                {
                    this.MostrarMensagem("Usuário não encontrado. Verifique seu login e tente novamente.", true);
                    return View(login);
                }
                var resultCredention = await _signInManager.PasswordSignInAsync(login.User, login.Password, login.Remember, false);
                if (resultCredention.Succeeded)
                {
                    login.ReturnUrl = login.ReturnUrl ?? "~/";
                    return RedirectToAction("Logged", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,
                        "Tentativa de login inválida. Reveja seus dados de acesso e tente novamente.");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }

    }
}
