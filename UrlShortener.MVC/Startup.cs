using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.DAL;
using UrlShortener.BLL;
using UrlShortener.BLL.Abstractions;
using UrlShortener.BLL.Services;

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
            services.AddHttpClient<CustomHttpClient>();
        }
    }
}
