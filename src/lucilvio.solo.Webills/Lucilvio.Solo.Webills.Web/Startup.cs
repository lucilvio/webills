using Lucilvio.Solo.Webills.Web.Home;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lucilvio.Solo.Webills.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Clear();
                options.ViewLocationFormats.Add("/Shared/{0}" + RazorViewEngine.ViewExtension);
                options.ViewLocationFormats.Add("/{1}/{0}/{0}" + RazorViewEngine.ViewExtension);
            });


            services.AddSingleton(new DataStorageContext());

            services.AddScoped<IAddNewIncomeDataStorage, AddNewIcomeDataStorageInMemory>();
            services.AddScoped<IAddNewExpenseDataStorage, AddNewExpenseDataStorage>();
            services.AddScoped<ISearchForUserIncomes, SearchForUserIncomesInMemory>();
            services.AddScoped<IAddNewIncome, AddNewIncome>();
            services.AddScoped<IAddNewExpense, AddNewExpense>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}", new
                {
                    Controller = "Home",
                    Action = "Index"
                });
            });
        }
    }
}