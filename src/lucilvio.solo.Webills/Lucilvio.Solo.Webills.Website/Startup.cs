using System;
using System.Linq;
using Lucilvio.Solo.Webills.EventBus;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Website.Shared;
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

        // This method gets called by the runtime. Use this method to add services to the container.
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

            services.AddSingleton<INotificationService>(new NotificationByEmailService(
                Environment.GetEnvironmentVariable("email_host", EnvironmentVariableTarget.User),
                int.Parse(Environment.GetEnvironmentVariable("email_port", EnvironmentVariableTarget.User)),
                Environment.GetEnvironmentVariable("email_user", EnvironmentVariableTarget.User),
                Environment.GetEnvironmentVariable("email_password", EnvironmentVariableTarget.User)
            ));

            IEventBus eventBus = new DefaultEventBus();

            var userAccountModule = new UserAccount.Module(eventBus, new UserAccount.Configurations
            {
                DefaultAccount = new UserAccount.Configurations.DefaultUserAccount
                {
                    Name = "Admin",
                    Password = "123456",
                    Email = "admin@mail.com",
                },
                DataConnection = Configuration.GetConnectionString("dataConnection"),
                CreateDefaultUserAccount = true,
            });

            var transactionsModule = new Solo.Webills.Transactions.Module(eventBus, new Lucilvio.Solo.Webills.Transactions.Configurations
            {
                DataConnection = Configuration.GetConnectionString("dataConnection")
            });

            services.AddSingleton<IEventBus>(eventBus);
            services.AddSingleton<UserAccount.Module>(userAccountModule);
            services.AddSingleton<Module>(transactionsModule);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
