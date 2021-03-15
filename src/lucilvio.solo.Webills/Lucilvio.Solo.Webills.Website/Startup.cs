using System;
using System.Linq;
using Lucilvio.Solo.Webills.Website.Shared.Authorization;
using Lucilvio.Solo.Webills.Website.Shared.Filters;
using Lucilvio.Solo.Webills.Website.Shared.Notification;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lucilvio.Solo.Webills.Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ITempDataDictionaryFactory TempDataDictionaryFactory { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                    options.ClaimsIssuer = "webills.com";
                });

            services.AddRazorPages(config =>
            {
                config.RootDirectory = "/";
                config.Conventions.AddPageRoute("/Home/Dashboard", "");
                config.Conventions.Add(new CustomPageRouteModelConvention());
            })
            .AddRazorPagesOptions(options =>
            {
                options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
                options.Conventions.ConfigureFilter(new AuthorizeFilter());
                options.Conventions.ConfigureFilter(new ExceptionsFilter());

            })
            .AddRazorRuntimeCompilation();

            services.AddSingleton<IAuthService, AuthService>();

            var emailHost = Environment.GetEnvironmentVariable("email_host", EnvironmentVariableTarget.User) ?? "";
            var port = Environment.GetEnvironmentVariable("email_port", EnvironmentVariableTarget.User) ?? "0";
            var user = Environment.GetEnvironmentVariable("email_user", EnvironmentVariableTarget.User) ?? "";
            var password = Environment.GetEnvironmentVariable("email_password", EnvironmentVariableTarget.User) ?? "";

            services.AddSingleton<INotificationService>(new NotificationByEmailService(
                emailHost, int.Parse(port), user, password));

            var userAccountModule = new UserAccount.Module(new UserAccount.Configurations
            {
                DefaultAccount = new UserAccount.Configurations.DefaultUserAccount
                {
                    Name = "Admin",
                    Password = "123456",
                    Email = "admin@mail.com",
                },
                DataConnectionString = this.Configuration.GetConnectionString("dataConnection")
            });

            var financialControlModule = new FinancialControl.Module(new Webills.FinancialControl.Configurations
            {
                DataConnectionString = this.Configuration.GetConnectionString("dataConnection")
            });

            services.AddSingleton(userAccountModule);
            services.AddSingleton(financialControlModule);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private class CustomPageRouteModelConvention : IPageRouteModelConvention
        {
            public void Apply(PageRouteModel model)
            {
                model.Selectors.ToList().ForEach(s =>
                {
                    var template = s.AttributeRouteModel.Template;

                    if (!template.Contains("/"))
                        return;

                    var segments = template.Split("/");

                    if (segments.Count() < 2)
                        return;

                    if (!segments[0].Equals(segments[1], StringComparison.InvariantCultureIgnoreCase))
                        return;

                    s.AttributeRouteModel.Template = $"{segments[0]}/";
                });
            }
        }
    }
}