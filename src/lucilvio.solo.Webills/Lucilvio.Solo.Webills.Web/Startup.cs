using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.UseCases.EditExpense;
using Lucilvio.Solo.Webills.UseCases.EditIncome;
using Lucilvio.Solo.Webills.Web.Home;
using Lucilvio.Solo.Webills.Web.Home.EditExpense;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

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
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.LoginPath = "/Login";
                });

            services.AddDbContext<WebillsContext>(options =>
            {
                options.UseSqlServer(this._configuration.GetConnectionString("Webills"));
            });

            services.AddScoped<IAddNewIncomeDataStorage, AddNewIncomeDataStorageWithEf>();
            services.AddScoped<IAddNewExpenseDataStorage, AddNewExpenseDataStorageWithEf>();
            services.AddScoped<IEditIncomeDataStorage, EditIncomeDataStorageWithEf>();
            services.AddScoped<IEditExpenseDataStorage, EditExpenseDataStorageWithEf>();
            services.AddScoped<ISearchForUserTransactionsInformation, SearchForUserTransactionsInformation>();
            services.AddScoped<ISearchForUserIncomeByNumber, SarchForUserIncomeByNumber>();
            services.AddScoped<ISearchForUserExpenseByNumber, SearchForUserExpenseByNumber>();
            services.AddScoped<IAddNewIncome, AddNewIncome>();
            services.AddScoped<IAddNewExpense, AddNewExpense>();
            services.AddScoped<IEditIncome, EditIncome>();
            services.AddScoped<IEditExpense, EditExpense>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}", new
                {
                    Controller = "Login",
                    Action = "Index"
                });
            });
        }
    }
}