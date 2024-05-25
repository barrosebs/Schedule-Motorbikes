using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Application.Extensions;
using SM.Domain.Enum;
using SM.Domain.Interface.IService;
using SM.Domain.Model;

namespace SM.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AllocationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceBase<MotorcycleModel> _motorcycleService;
        private readonly IServiceBase<AllocationModel> _allocationService;
        private readonly IServiceBase<PlanModel> _planService;

        public AllocationController(
                                        IServiceBase<AllocationModel> allocationService,
                                        IMapper mapper,
                                        IServiceBase<MotorcycleModel> motorcycleService,
                                        IServiceBase<PlanModel> planService
                                    )
        {
            _allocationService = allocationService;
            _mapper = mapper;
            _motorcycleService = motorcycleService;
            _planService = planService;
        }
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var listMotorcycle = await _motorcycleService.GetAllAsync();
            var listPlans = await _planService.GetAllAsync();
            ViewBag.AllocationPlano = listPlans;
            ViewBag.ListMotorcycle = listMotorcycle;
            return View();
        }
    }
}
