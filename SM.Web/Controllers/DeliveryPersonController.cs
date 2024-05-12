using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Domain.Enum;
using SM.Domain.Interface.IService;
using SM.Domain.Model;
using SM.Application.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace SM.Web.Controllers
{
    [Authorize]
    public class DeliveryPersonController : Controller
    {
        private readonly IServiceBase<DeliveryPersonModel> _deliveryPerson;
        private readonly IMapper _mapper;

        public DeliveryPersonController(IServiceBase<DeliveryPersonModel> deliveryPerson, IMapper mapper)
        {
            _deliveryPerson = deliveryPerson;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Lista Motoboy";

            var model = _deliveryPerson.GetAllAsync().GetAwaiter().GetResult();
            var viewModel = _mapper.Map<IEnumerable<DeliveryPersonVM>>(model);
            return View(viewModel);
        }
        [HttpGet("[controller]/Create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Cria Motoboy";

            var typeCNH = Enum.GetValues(typeof(ETypeCNH));
            List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
            ViewBag.TypeCNH = _typeCNH;
            return View();
        }
        [HttpPost("[controller]/Create")]
        public async Task<IActionResult> Create([FromForm] DeliveryPersonVM viewModel)
        {
            if (ModelState.IsValid)
            {
                DeliveryPersonModel model = _mapper.Map<DeliveryPersonModel>(viewModel);
                await _deliveryPerson.CreateAsync(model);
                this.MostrarMensagem("Dados do motoboy salvo com sucesso.");
                return RedirectToAction("Index", "DeliveryPerson");
            }
            else
            {
                this.MostrarMensagem("Erro ao tentar Gravas dados do motoboy!.", true);
                foreach (var error in ModelState)
                {
                    ModelState.AddModelError(string.Empty, error.Key);
                }
                return View();
            }
        }
    }
}
