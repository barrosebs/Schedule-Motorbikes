using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Domain.Enum;
using SM.Domain.Interface.IService;
using SM.Domain.Model;
using SM.Application.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SM.Domain.Models;
using SM.Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace SM.Web.Controllers
{
    [Authorize(Roles = nameof(ERole.Administrator))]
    [Route("[controller]")]
    public class DeliveryPersonController : Controller
    {
        private readonly IDeliveryPersonService _deliveryPerson;
        private readonly IMapper _mapper;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeliveryPersonController(IDeliveryPersonService deliveryPerson, IMapper mapper, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _deliveryPerson = deliveryPerson;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Lista Motoboy";

            var model = _deliveryPerson.GetAllAsync().GetAwaiter().GetResult();
            var viewModel = _mapper.Map<IEnumerable<DeliveryPersonVM>>(model);
            return View(viewModel);
        }
        public IActionResult Create()
        {
            ViewData["Title"] = "Cria Motoboy";

            var typeCNH = Enum.GetValues(typeof(ETypeCNH));
            List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
            ViewBag.TypeCNH = _typeCNH;
            return View();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] DeliveryPersonVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var typeCNH = Enum.GetValues(typeof(ETypeCNH));
                    List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
                    ViewBag.TypeCNH = _typeCNH;

                    var Claims = _httpContextAccessor.HttpContext?.User;
                    var userId = _userManager.GetUserId(Claims);
                    DeliveryPersonModel model = _mapper.Map<DeliveryPersonModel>(viewModel);
                    model.UrlImageCNH = ToolsHelpers.UploadFile(viewModel.ImageCNH, viewModel.NumberCNH);
                    model.DateCreated = DateTime.Now;
                    model.UserId = int.Parse(userId);
                    model.NumberCNPJ = ToolsHelpers.RemoveMaskCNPJ(viewModel.NumberCNPJ);
                    await _deliveryPerson.CreateAsync(model);

                    this.ShowMessage("Dados do motoboy salvo com sucesso.");
                    return RedirectToAction("Index", "DeliveryPerson");
                }
                catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException postgresEx && postgresEx.SqlState == "23505")
                {
                    if (postgresEx.ConstraintName == "IX_DeliveryPeople_NumberCNPJ")
                        ModelState.AddModelError("NumberCNPJ", "O número de CNPJ já está em uso.");
                    if (postgresEx.ConstraintName == "IX_DeliveryPeople_NumberCNH")
                        ModelState.AddModelError("NumberCNH", "O número de CNH já está em uso.");

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
                this.ShowMessage("Erro ao tentar Gravas dados do motoboy!.", true);
                foreach (var error in ModelState)
                {
                    ModelState.AddModelError(string.Empty, error.Key);
                }
                return View();
            }
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    this.ShowMessage("Motoboy não informado.", true);
                    return RedirectToAction(nameof(Index));
                }
                DeliveryPersonModel? DeliveryPersonModal = await _deliveryPerson.GetByIdAsync(id);
                if (DeliveryPersonModal == null)
                {
                    this.ShowMessage("Entregador não encontrado.", true);
                    return RedirectToAction(nameof(Index));
                }

                await _deliveryPerson.RemoveAsync(DeliveryPersonModal);
                this.ShowMessage("Entregador Deletado com sucesso.", false);
                return RedirectToAction("Index", "DeliveryPerson");
            }
            catch (InvalidOperationException ex)
            {
                this.ShowMessage(ex.Message, true);
                return RedirectToAction("Index", "DeliveryPerson");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        [Authorize(Roles = nameof(ERole.DeliveryPerson))]
        [HttpGet("Update/{email}")]
        public async Task<IActionResult> Update(string email)
        {
            try
            {

                DeliveryPersonModel model = await _deliveryPerson.GetDeliveryPersonByEmail(email);
                if (model != null)
                {
                    DeliveryPersonVM viewModel = _mapper.Map<DeliveryPersonVM>(model);
                    var typeCNH = Enum.GetValues(typeof(ETypeCNH));
                    List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
                    ViewBag.TypeCNH = _typeCNH;

                    return View("Update",viewModel);

                }
                this.ShowMessage("Entregador não encontrado!.", false);
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        [Authorize(Roles = nameof(ERole.DeliveryPerson))]
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update([FromForm] DeliveryPersonVM viewModel)
        {
            try
            {
                Guid viewModelId = Guid.Parse(viewModel.Id);
                
                DeliveryPersonModel model = await _deliveryPerson.GetByIdAsync(viewModelId);
                var Claims = _httpContextAccessor.HttpContext?.User;
                var userId = _userManager.GetUserId(Claims);
                var typeCNH = Enum.GetValues(typeof(ETypeCNH));
                List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
                ViewBag.TypeCNH = _typeCNH;
                model.UrlImageCNH = ToolsHelpers.UploadFile(viewModel.ImageCNH, viewModel.NumberCNH).ToString();
                model.UserId = int.Parse(userId);
                await _deliveryPerson.UpdateAsync(model);

                this.ShowMessage("CNH atualizada com sucesso!", false);
                return RedirectToAction("Logged","Home");
            }
            catch (Exception ex)
            {
                this.ShowMessage(ex.Message, true);
                return RedirectToAction("Logged", "Home");
            }


        }
    }
}
