using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.DAL;
using UrlShorter.BLL;
using UrlShorter.BLL.Abstractions;
using UrlShorter.BLL.Services;

namespace UrlShortener.MVC
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore().AddRazorViewEngine();
            services.AddControllersWithViews();

            services.AddSqlStorage(configuration);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => options.LoginPath = "/login");

            services.AddHttpContextAccessor();
            services.AddTransient<IUserContext, UserContext>();

            services.AddBLLServices(configuration);
        }
    }
}
