using System.Globalization;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Notification;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Lucilvio.Solo.Webills.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Clear();
                options.ViewLocationFormats.Add("/Shared/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/{1}/{0}/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/{1}/{0}" + RazorViewEngine.ViewExtension);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                });

            services.AddHttpContextAccessor();

            services.AddSingleton<IAuthenticationService>(service =>
            {
                return new AuthenticationService(service.GetService<IHttpContextAccessor>());
            });

            var host = this._configuration.GetSection("email").GetSection("host").Value;
            var port = int.Parse(this._configuration.GetSection("email").GetSection("port").Value);
            var user = this._configuration.GetSection("email").GetSection("user").Value;
            var password = this._configuration.GetSection("email").GetSection("password").Value;

            var notificationService = new NotificationByEmailService(host, port, user, password);

            services.AddSingleton(svc => notificationService);

            services.AddModules(this._configuration, notificationService);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-PT");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-PT");

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}", new
                {
                    Action = "Index"
                });

                endpoints.MapControllerRoute("home", "{controller}/{action}/{id?}", new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            });
        }
    }
}