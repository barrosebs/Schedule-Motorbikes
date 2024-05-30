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

                    this.ShowMessage("Dados da moto salvo com sucesso.");
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
                    this.ShowMessage("ERRO:" + ex.Message, true);
                    return View();
                }

            }
            else
            {
                this.ShowMessage("Erro ao tentar Gravas dados da moto!.", true);
                foreach (var error in ModelState)
                {
                    ModelState.AddModelError(string.Empty, error.Key);
                }
                return View();
            }
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            MotorcycleModel? model = await _motorcycle.GetByIdAsync(id);
            if (model == null)
            {
                this.ShowMessage("Moto não locazada.", true);
                return RedirectToAction(nameof(Index));
            }
            MotorcycleVM modelView = _mapper.Map<MotorcycleVM>(model);
            return View(modelView);
        }
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update(string licensePlate, Guid id)
        {
            MotorcycleModel? model = await _motorcycle.GetByIdAsync(id);

            if (model?.LicensePlate != null) { 
                model.LicensePlate = licensePlate.ToUpper(); 
                await _motorcycle.UpdateAsync(model);
            }
            else {
                this.ShowMessage("Moto não locazada.", true);
                return View();
            }
            

            return RedirectToAction(nameof(Index));
        }


        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    this.ShowMessage("Moto não informado.", true);
                    return RedirectToAction(nameof(Index));
                }
                MotorcycleModel? model = await _motorcycle.GetByIdAsync(id);
                if (model == null)
                {
                    this.ShowMessage("Motocicleta não encontrado.", true);
                    return RedirectToAction(nameof(Index));
                }

                await _motorcycle.RemoveAsync(model);
                this.ShowMessage("Motocicleta Deletado com sucesso.", false);
                return RedirectToAction("Index", "Motorcycle");
            }
            catch (InvalidOperationException ex)
            {
                this.ShowMessage(ex.Message, true);
                return RedirectToAction("Index", "Motorcycle");
            }
            
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
