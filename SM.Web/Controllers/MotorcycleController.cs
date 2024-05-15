using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SM.Application.Extensions;
using SM.Domain.Enum;
using SM.Domain.Interface.IService;
using SM.Domain.Model;
using SM.Domain.Models;
using SM.Web.Helpers;

namespace SM.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MotorcycleController : Controller
    {
        private readonly IServiceBase<MotorcycleModel> _motorcycle;
        private readonly IMapper _mapper;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MotorcycleController(IServiceBase<MotorcycleModel> motorcycle, IMapper mapper, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _motorcycle = motorcycle;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Lista Motos";

            var model = _motorcycle.GetAllAsync().GetAwaiter().GetResult();
            var viewModel = _mapper.Map<IEnumerable<MotorcycleVM>>(model);
            return View(viewModel);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Cria Moto";
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] MotorcycleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Claims = _httpContextAccessor.HttpContext?.User;
                    var userId = _userManager.GetUserId(Claims);
                    MotorcycleModel model = _mapper.Map<MotorcycleModel>(viewModel);
                    model.DateCreated = DateTime.Now;
                    model.UserId = int.Parse(userId);
                    await _motorcycle.CreateAsync(model);

                    this.MostrarMensagem("Dados da moto salvo com sucesso.");
                    return RedirectToAction("Index", "Motorcycle");
                }
                catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresEx && postgresEx.SqlState == "23505")
                {
                    if (postgresEx.ConstraintName == "IX_DeliveryPeople_LicensePlate")
                        ModelState.AddModelError("LicensePlate", "A placa já está em uso.");
                    return View();
                }
                catch (Exception ex)
                {
                    this.MostrarMensagem("ERRO:" + ex.Message, true);
                    return View();
                }

            }
            else
            {
                this.MostrarMensagem("Erro ao tentar Gravas dados da moto!.", true);
                foreach (var error in ModelState)
                {
                    ModelState.AddModelError(string.Empty, error.Key);
                }
                return View();
            }
        }

    }
}
