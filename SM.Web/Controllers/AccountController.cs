using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SM.Domain.Models;
using SM.Domain.ViewModel;
using SM.Application.Extensions;
using SM.Domain.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Domain.Enum;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SM.Web.Helpers;
using SM.Domain.Interface.IService;
using Microsoft.AspNetCore.Authorization;

namespace SM.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private readonly IMapper _mapper;
        private readonly IDeliveryPersonService _deliveryPerson;

        public AccountController(
                                    UserManager<UserModel> userManager,
                                    SignInManager<UserModel> signInManager,
                                    IMapper mapper,
                                    IDeliveryPersonService deliveryPerson
                                )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _deliveryPerson = deliveryPerson;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            @ViewData["Title"] = "Login";
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.User);
                if (user == null)
                {
                    this.ShowMessage("Usuário não encontrado. Verifique seu login e tente novamente.", true);
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
                        "Tentativa de login inválida. Reveja seus viewModel de acesso e tente novamente.");
                    return View(login);
                }
            }
            else
            {
                return View(login);
            }
        }
        [HttpGet("Create")]
        public IActionResult Create()
        {
            var typeCNH = Enum.GetValues(typeof(ETypeCNH));
            List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
            ViewBag.TypeCNH = _typeCNH;
            return View();
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] DeliveryPersonVM viewModel)
        {
            @ViewData["Title"] = "Criar Usuario";

            try
            {
                var typeCNH = Enum.GetValues(typeof(ETypeCNH));
                List<SelectListItem> _typeCNH = EnumExtensios.GetDescriptionEnum(typeCNH);
                ViewBag.TypeCNH = _typeCNH;

                IFormFile file = viewModel.ImageCNH;
                string extension = Path.GetExtension(file.FileName);
                if (extension != ".png" || extension == ".bmp")
                    throw new ApplicationException(message: "Arquivo não é válido!");
                if (viewModel.DateValidationCNH <= DateTime.Now.AddDays(15).AddMilliseconds(0))
                    throw new ApplicationException(message: "CNH inválida. Sua CNH esta vencida ou próxima (15 dias) do vencimento ");

                DeliveryPersonModel model = _mapper.Map<DeliveryPersonModel>(viewModel);
                model.UrlImageCNH = ToolsHelpers.UploadFile(viewModel.ImageCNH, viewModel.NumberCNH);
                model.DateCreated = DateTime.Now;
                model.NumberCNPJ = ToolsHelpers.RemoveMaskCNPJ(viewModel.NumberCNPJ);
                await _deliveryPerson.CreateAsync(model);

                var modelUserDelivery = _mapper.Map<UserDeliveryPersonVM>(viewModel);
                var email = await CreateUserAsync(modelUserDelivery);
                return RedirectToAction(nameof(ResetPassword), new { email });

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

        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            @ViewData["Title"] = "Criar senha";

            var isUser = _userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (email == null)
                throw new ApplicationException("Email é obrigatório!");
            
            string token = _userManager.GeneratePasswordResetTokenAsync(isUser).GetAwaiter().GetResult();
            
            ResetPasswordVM resetPasswordVM = _mapper.Map<ResetPasswordVM>(isUser);
            resetPasswordVM.Email = email;
            resetPasswordVM.Token = token;

            return View(resetPasswordVM);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                var result = await _userManager.ResetPasswordAsync(
                    user, viewModel.Token, viewModel.NewPassword);
                if (result.Succeeded)
                {
                    this.ShowMessage(
                       $"Usuário criado com sucesso! Agora você já pode fazer login no sistema.");
                    return View(nameof(Login));
                }
                else
                {
                    this.ShowMessage(
                        $"Não foi possível redefinir a senha. Verifique se preencheu a senha corretamente. Se o problema persistir, entre em contato com o suporte.");
                    return View(viewModel);
                }
            }
            else
            {
                return View(viewModel);
            }
        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        [NonAction]
        public async Task<string> CreateUserAsync(
            UserDeliveryPersonVM viewModel)
        {

            if (string.IsNullOrEmpty(viewModel.Email))
                throw new ApplicationException("Email é obrigatório!");

            var modelUser = _mapper.Map<UserModel>(viewModel);
            UserModel? userBD = _userManager.Users.FirstOrDefault(x => x.Email == modelUser.Email.ToUpper().Trim());
            if (userBD != null)
            {
                if (_userManager.Users.Any(u => u.NormalizedEmail == viewModel.Email.ToUpper().Trim()))
                {
                    ModelState.AddModelError("Email",
                        "Já existe um usuário cadastrado com este e-mail.");
                    return string.Empty;
                }
            }
            modelUser.UserName = modelUser.Email;
            string password = Guid.NewGuid().ToString().Substring(0, 8);

            var userCreated = await _userManager.CreateAsync(modelUser, password);

            if (userCreated.Succeeded)
            {
                UserModel? isUser = await _userManager.FindByEmailAsync(modelUser.Email);
                if (isUser != null)
                {
                    _userManager.AddToRoleAsync(isUser, Enum.GetName(typeof(ERole), ERole.DeliveryPerson)).Wait();
                    var DeliveryPersonClaim = new Claim(ClaimTypes.Role,
                        isUser.Email);
                    isUser.EmailConfirmed = true;
                    _userManager.AddClaimAsync(isUser, DeliveryPersonClaim).Wait();

                    await _signInManager.SignOutAsync();
                    return isUser.Email;
                }
            }
            else
            {
                foreach (var error in userCreated.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                throw new ApplicationException($"Erro ao cadastrar usuário.");
            }
            return null;
        }
        [HttpGet("RestrictedAccess")]
        public IActionResult RestrictedAccess([FromQuery] string returnUrl)
        {
            return View(model: returnUrl);
        }

    }
}
