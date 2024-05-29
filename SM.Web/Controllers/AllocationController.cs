using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Application.Extensions;
using SM.Domain.Enum;
using SM.Domain.Interface.IService;
using SM.Domain.Interface.IServices;
using SM.Domain.Model;
using SM.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SM.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AllocationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceBase<MotorcycleModel> _motorcycleService;
        private readonly IAllocationService _allocationService;
        private readonly IDeliveryPersonService _deliveryPersonService;
        private readonly IPlanService _planService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserModel> _userManager;

       public AllocationController(
                                        IAllocationService allocationService,
                                        IMapper mapper,
                                        IServiceBase<MotorcycleModel> motorcycleService,
                                        IPlanService planService,
                                        IHttpContextAccessor httpContextAccessor,
                                        UserManager<UserModel> userManager,
                                        IDeliveryPersonService deliveryPersonService)
        {
            _allocationService = allocationService;
            _mapper = mapper;
            _motorcycleService = motorcycleService;
            _planService = planService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _deliveryPersonService = deliveryPersonService;
        }
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var listMotorcycle = await _motorcycleService.GetAllAsync();
            var listPlans = await _planService.GetAllAsync();
            listPlans = listPlans.OrderBy(x => x.LimitDayPlan);
            ViewBag.AllocationPlano = listPlans;
            ViewBag.ListMotorcycle = listMotorcycle;
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] AllocationVM modelView)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel? userLogIn = GetUser();
                    var listPlan = await _planService.GetAllAsync();
                    var deliveryPerson = _deliveryPersonService.GetDeliveryPersonByEmail(userLogIn.Email).GetAwaiter().GetResult();
                    var listMotorcycle = await _motorcycleService.GetAllAsync();
                    var listPlans = await _planService.GetAllAsync();
                    ViewBag.AllocationPlano = listPlans;
                    ViewBag.ListMotorcycle = listMotorcycle;

                    if (deliveryPerson.TypeCNH == ETypeCNH.B)
                    {
                        this.ShowMessage($"Você não tem permissão para alocar motocicleta. Categoria de CNH é inválida", true);
                        return View();
                    }
                    if (deliveryPerson.DateValidationCNH.Ticks <= DateTime.Now.Ticks)
                    {
                        this.ShowMessage($"CNH com data de validade vencida.", true);
                        return View();
                    }
                    PlanModel planModel = listPlan.First(x => x.EPlan == modelView.EPlan);
                    string startDate = modelView.StartDateToAllocation.AddDays(planModel.LimitDayPlan + 1).Ticks.ToString();
                    string endDate = modelView.EndDateToAllocation.Ticks.ToString();
                    if (startDate != endDate)
                    {
                        this.ShowMessage($"Data prevista de entrega incorreta. A Data correta conforme o plano escolhido:{modelView.StartDateToAllocation.AddDays(planModel.LimitDayPlan + 1).ToString("D")}", true);
                        return View();
                    }
                    modelView.DeliveryPerson = deliveryPerson;
                    AllocationModel model = _mapper.Map<AllocationModel>(modelView);
                    model.IsAllocation = true;
                    await _allocationService.CreateAsync(model);
                    
                    userLogIn.HasAllocation = true;
                    await _userManager.UpdateAsync(userLogIn);
                    
                    this.ShowMessage("Alocação realizada com Sucesso!");
                    return RedirectToAction("Logged", "Home");
                }
                else
                {
                    this.ShowMessage(ModelState.ValidationState.ToString(), true);
                    return View(modelView);
                }
                throw new ValidationException("Um ou mais erros de validação ocorreram.");
            }
            catch (ValidationException ex)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                                       .ToDictionary(
                                            kvp => kvp.Key,
                                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                                       );

                var errorResponse = new
                {
                    title = "Erro de validação.",
                    status = 400,
                    errors = errors,
                    traceId = HttpContext.TraceIdentifier
                };

                return BadRequest(errorResponse);
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        [HttpGet("Deallocate")]
        public IActionResult Deallocate()
        {
            var allocationActive = _allocationService.GetAllocationActiveAsync().GetAwaiter().GetResult();
            var plan = _planService.GetPlanByPlanAsync(allocationActive.EPlan).GetAwaiter().GetResult();
            if (allocationActive != null)
            {
                AllocationVM viewModel = _mapper.Map<AllocationVM>(allocationActive);
                viewModel.Plan = plan.ToString();
                viewModel.DeliveryDate = DateTime.Now;
                return View(viewModel);
            }
            this.ShowMessage("Não existe moto alugada", true);

            return RedirectToAction("Logged","Home");

        }
        [HttpPost("CheckAmountToPay")]
        public IActionResult CheckAmountToPay([FromForm] AllocationVM viewModel)
        {
            var allocationActive = _allocationService.GetAllocationActiveAsync().GetAwaiter().GetResult();
            var plan = _planService.GetPlanByPlanAsync(allocationActive.EPlan).GetAwaiter().GetResult();
            if (allocationActive != null)
            {
                allocationActive.DeliveryDate = viewModel.DeliveryDate;
                viewModel = _mapper.Map<AllocationVM>(allocationActive);
                viewModel.Plan = plan.ToString();
                viewModel = CalcDays(viewModel, allocationActive, plan);
            }

            return View("Deallocate", viewModel);
        }
        [NonAction]
        private AllocationVM CalcDays(AllocationVM viewModel, AllocationModel? allocationActive, PlanModel plan)
        {
            DateTime startDate = allocationActive.StartDateToAllocation.AddDays(1);
            TimeSpan usedDays = viewModel.DeliveryDate - startDate;
            decimal remainingDays =(decimal)plan.LimitDayPlan - (decimal)usedDays.TotalDays;
            viewModel.UsedDays = usedDays.Days;
             decimal resultPlan = CalcToPlan(viewModel, plan, remainingDays);
            if (usedDays.TotalDays < 7)
            {
                viewModel.Sum = (decimal)usedDays.TotalDays * plan.Value + resultPlan;
                viewModel.ValueDay += plan.Value * (decimal)viewModel.UsedDays;

            }
            else if (plan.LimitDayPlan == usedDays.TotalDays)
            {
                viewModel.UsedDays = usedDays.TotalDays - plan.LimitDayPlan;
                viewModel.Sum = plan.Value * plan.LimitDayPlan;
                viewModel.ValueDay = plan.Value * plan.LimitDayPlan;
            }
            else
            {
                viewModel.UsedDays = usedDays.TotalDays;
                viewModel.ValueDay = (decimal)viewModel.UsedDays * plan.Value;
                viewModel.Sum = viewModel.ValueDay + 50;
            }

            return viewModel;
        }

        private static decimal CalcToPlan(AllocationVM viewModel, PlanModel plan, decimal remainingDays)
        {
            if (viewModel.EPlan == EAllocationPlan.Basic)
                viewModel.DailyRate = 0.20m;

            if (viewModel.EPlan == EAllocationPlan.Standard)
                viewModel.DailyRate = 0.40m;
            
            decimal remainingDaysTotalWithPenalty = (decimal)remainingDays * plan.Value * (viewModel.DailyRate);
            viewModel.Sum = viewModel.ValueDay + remainingDaysTotalWithPenalty;
            return viewModel.Sum;
        }

        [NonAction]
        private UserModel? GetUser()
        {
            ClaimsPrincipal claims = _httpContextAccessor.HttpContext.User;
            var userLogIn = _userManager.GetUserAsync(claims).GetAwaiter().GetResult();
            return userLogIn;
        }

    }
}
