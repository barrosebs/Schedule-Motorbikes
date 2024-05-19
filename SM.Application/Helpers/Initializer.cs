using Microsoft.AspNetCore.Identity;
using SM.Domain.Enum;
using SM.Domain.Models;
using System.Security.Claims;

namespace SM.Application.Helpers
{
    public static class Initializer
    {
        private static void InitializerPerfis(RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.RoleExistsAsync(Enum.GetName(typeof(ERole), ERole.Administrator)).GetAwaiter().GetResult())
            {
                var perfil = new IdentityRole<int>();
                perfil.Name = Enum.GetName(typeof(ERole), ERole.Administrator);
                roleManager.CreateAsync(perfil).Wait();
            }
            if (!roleManager.RoleExistsAsync(Enum.GetName(typeof(ERole), ERole.DeliveryPerson)).GetAwaiter().GetResult())
            {
                var perfil = new IdentityRole<int>();
                perfil.Name = Enum.GetName(typeof(ERole), ERole.DeliveryPerson);
                roleManager.CreateAsync(perfil).Wait();
            }
        }
        private static void InitializerUserAdmin(UserManager<UserModel> userManager)
        {

            if (userManager.FindByNameAsync("barrosebs+admin@gmail.com").GetAwaiter().GetResult() == null)
            {
                var user = new UserModel();
                user.UserName = "admin";
                user.Email = "barrosebs+admin@gmail.com";
                user.FullName = "Administrador do Sistema";
                user.DateOfBirth = new DateTime(1980, 2, 27);
                user.PhoneNumber = "71996407566";
                user.EmailConfirmed = true;
                var result = userManager.CreateAsync(user, "123Aa@").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Enum.GetName(typeof(ERole), ERole.Administrator)).Wait();
                    var DateOfBirthClaim = new Claim(ClaimTypes.DateOfBirth,
                        user.DateOfBirth.Date.ToShortDateString());
                    userManager.AddClaimAsync(user, DateOfBirthClaim).Wait();
                }
            }
        }
        public static void InicializarIdentity(
                UserManager<UserModel> userManager,
                RoleManager<IdentityRole<int>> roleManager
            )
        {
            InitializerPerfis(roleManager);
            InitializerUserAdmin(userManager);
        }

    }
}
