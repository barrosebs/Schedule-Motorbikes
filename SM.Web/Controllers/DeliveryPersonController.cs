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
using System.IO.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace SM.Web.Controllers
{
    [Authorize(Roles = nameof(ERole.Administrator))]
    [Route("[controller]")]
    public class DeliveryPersonController : Controller
    {
        private readonly IServiceBase<DeliveryPersonModel> _deliveryPerson;
        private readonly IMapper _mapper;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeliveryPersonController(IServiceBase<DeliveryPersonModel> deliveryPerson, IMapper mapper, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
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

                    IFormFile file = viewModel.ImageCNH;
                   string extension = Path.GetExtension(file.FileName);
                    if (extension != ".png" || extension == ".bmp")
                    throw new ApplicationException("Arquivo não é válido!");

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
                }catch (DbUpdateException ex) when(ex.InnerException is Npgsql.PostgresException postgresEx && postgresEx.SqlState == "23505")
                {
                    if(postgresEx.ConstraintName == "IX_DeliveryPeople_NumberCNPJ")
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
        /// <summary>
        /// Por padrão, o protocolo HTTP esta usando o method POST e GET, com isso não pude fazer uso de outros verbos como PUT e DELETE.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                this.ShowMessage("Motoboy não informado.", true);
                return RedirectToAction(nameof(Index));
            }
            DeliveryPersonModel? DeliveryPersonModal = await _deliveryPerson.GetByIdAsync(id);
            if (DeliveryPersonModal == null)
            {
                this.ShowMessage("Motoboy não encontrado.", true);
                return RedirectToAction(nameof(Index));
            }
            
            await _deliveryPerson.RemoveAsync(DeliveryPersonModal);
            this.ShowMessage("Motoboy Deletado.", false);
            return RedirectToAction("Index", "DeliveryPerson");
        }

        /// <summary>
        /// Por padrão, o protocolo HTTP esta usando o method POST e GET, com isso não pude fazer uso de outros verbos como PUT e DELETE.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Update/{id}")]
        public async Task<IActionResult> Update([FromForm] DeliveryPersonVM viewModel)
        {
            try
            {
                this.ShowMessage("Motoboy Deletado.", false);
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}
