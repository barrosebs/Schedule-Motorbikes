using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Mapper;
using SM.Data.Context;
using SM.Domain.Models;
using SM.Services.Extensions;


namespace SM.Services.IoC
{
    public static class NativeInjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SMContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("SMConnection"))
                       .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted })
                       .EnableSensitiveDataLogging()

            );

            services.AddIdentity<User, IdentityRole<int>>(options =>
                    {
                        options.User.RequireUniqueEmail = true; //false
                        options.User.AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //idem
                        options.Password.RequireNonAlphanumeric = false; //true
                        options.Password.RequireUppercase = false; //true;
                        options.Password.RequireLowercase = false; //true;
                        options.Password.RequireDigit = false; //true;
                        options.Password.RequiredUniqueChars = 1; //1;
                        options.Password.RequiredLength = 6; //6;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); 
                        options.Lockout.MaxFailedAccessAttempts = 5; //5
                        options.Lockout.AllowedForNewUsers = true; //true		
                        options.SignIn.RequireConfirmedEmail = true; //false
                        options.SignIn.RequireConfirmedPhoneNumber = false; //false
                        options.SignIn.RequireConfirmedAccount = false; //false
                    })
                .AddEntityFrameworkStores<SMContext>()
                .AddDefaultTokenProviders();

            //services.AddScoped<IServiceBase<Menu>, MenuService>();

            //services.AddTransient<SeedingRepository>();
            //services.AddScoped<IMenuRepository, MenuRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapperApi(typeof(MapperProfile));
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AppControleUsuarios"; //AspNetCore.Cookies
                options.Cookie.HttpOnly = true; //true
                options.ExpireTimeSpan = TimeSpan.FromHours(4); //14 dias
                options.LoginPath = "/Usuario/Login"; // /Account/Login
                options.LogoutPath = "/Home/Principal";  // /Account/Logout
                options.AccessDeniedPath = "/Usuario/AcessoRestrito"; // /Account/AccessDenied
                options.SlidingExpiration = true; //true - gera um novo cookie a cada requisição se o cookie estiver com menos de meia vida
                options.ReturnUrlParameter = "returnUrl"; //returnUrl
            });
        }
    }
}