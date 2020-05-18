using System.Globalization;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Filters;
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
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new ErrorFilter());
            }).AddRazorRuntimeCompilation();

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

            var host = this._configuration.GetSection("email").GetSection("host").Value;
            var port = int.Parse(this._configuration.GetSection("email").GetSection("port").Value);
            var user = this._configuration.GetSection("email").GetSection("user").Value;
            var password = this._configuration.GetSection("email").GetSection("password").Value;

            var svc = services.BuildServiceProvider();

            var notificationService = new NotificationByEmailService(host, port, user, password);
            var authService = new AuthenticationService(svc.GetService<IHttpContextAccessor>());

            services.AddSingleton<INotificationService>(svc => notificationService);
            services.AddSingleton<IAuthenticationService>(svc => authService);

            services.AddModules(this._configuration, notificationService, authService);
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