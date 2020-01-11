﻿using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.Logon;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;
using Lucilvio.Solo.Webills.UseCases.EditExpense;
using Lucilvio.Solo.Webills.UseCases.EditIncome;
using Lucilvio.Solo.Webills.UseCases.RemoveExpense;
using Lucilvio.Solo.Webills.UseCases.RemoveIncome;
using Lucilvio.Solo.Webills.UseCases.Logon;
using Lucilvio.Solo.Webills.Web.Home;
using Lucilvio.Solo.Webills.Web.Home.EditExpense;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.Web.Home.Sample;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using Lucilvio.Solo.Webills.Web.Logon;
using Microsoft.AspNetCore.Http;

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
                    options.LoginPath = "/Logon";
                });

            services.AddDbContext<WebillsContext>(options =>
            {
                options.UseSqlServer(this._configuration.GetConnectionString("Webills"));
            });

            services.AddDbContext<WebillsCoreContext>(options =>
            {
                options.UseSqlServer(this._configuration.GetConnectionString("Webills"));
            });

            services.AddDbContext<WebillsProfileContext>(options =>
            {
                options.UseSqlServer(this._configuration.GetConnectionString("Webills"));
            });

            services.AddDbContext<WebillsSecurityContext>(options =>
            {
                options.UseSqlServer(this._configuration.GetConnectionString("Webills"));
            });

            services.AddTransient(readContext =>
            {
                return new WebillsReadContext(this._configuration.GetConnectionString("Webills"));
            });

            services.AddHttpContextAccessor();

            services.AddScoped<IAuthentication>(service =>
            {
                return new SecurityService(service.GetService<IHttpContextAccessor>().HttpContext);
            });

            services.AddScoped<IAddNewIncomeDataStorage, AddNewIncomeDataStorageWithEf>();
            services.AddScoped<IAddNewExpenseDataStorage, AddNewExpenseDataStorageWithEf>();
            services.AddScoped<IEditIncomeDataStorage, EditIncomeDataStorageWithEf>();
            services.AddScoped<IEditExpenseDataStorage, EditExpenseDataStorageWithEf>();
            services.AddScoped<IRemoveIncomeDataStorage, RemoveIncomeDataStorageWithEf>();
            services.AddScoped<IRemoveExpenseDataStorage, RemoveExpenseDataStorageWithEf>();
            services.AddScoped<IUserDashboardQueryHandler, UserTransactionsInformationQuery>();
            services.AddScoped<IGetUserIncomesQueryHandler, GetUserIncomeQueryHandler>();
            services.AddScoped<IGetUserExpensesQueryHandler, GetUserExpensesQueryHandler>();
            services.AddScoped<IAddNewIncome, AddNewIncome>();
            services.AddScoped<IAddNewExpense, AddNewExpense>();
            services.AddScoped<IEditIncome, EditIncome>();
            services.AddScoped<IEditExpense, EditExpense>();
            services.AddScoped<IRemoveIncome, RemoveIncome>();
            services.AddScoped<IRemoveExpense, RemoveExpense>();
            services.AddScoped<ILogon, Webills.UseCases.Logon.Logon>();
            services.AddScoped<ILogonDataStorage, LogonDataStorage>();
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
                    Action = "Dashboard"
                });
            });
        }
    }
}