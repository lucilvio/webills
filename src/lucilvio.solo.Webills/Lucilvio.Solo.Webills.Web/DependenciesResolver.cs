using Lucilvio.Solo.Webills.UseCases.Logon;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lucilvio.Solo.Webills.Web.Logon;
using Microsoft.AspNetCore.Http;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.Core.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.Core.UseCases.EditIncome;
using Lucilvio.Solo.Webills.Core.UseCases.EditExpense;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveIncome;
using Lucilvio.Solo.Webills.Core.UseCases.RemoveExpense;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditExpense;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense;
using Lucilvio.Solo.Webills.Security.UseCases.Contracts.Logon;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Core;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Profile;
using Lucilvio.Solo.Webills.Infraestructure.EFDataStorage.Security;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Clients.Web.Incomes;

namespace Lucilvio.Solo.Webills.Web
{
    public static class DependenciesResolver
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebillsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Webills"));
            });

            services.AddTransient(readContext =>
            {
                return new WebillsReadContext(configuration.GetConnectionString("Webills"));
            });
            
            services.AddScoped<IUserDashboardQueryHandler, UserTransactionsInformationQuery>();
            services.AddScoped<IGetUserIncomesByFilterQueryHandler, GetUserIncomesByFilterQueryHandler>();
            services.AddScoped<IGetUserExpensesByFilterQueryHandler, GetUserExpensesByFilterQueryHandler>();

            ResolveCoreDependencies(services, configuration);
            ResolveProfileDependencies(services, configuration);
            ResolveSecurityDependencies(services, configuration);
        }

        private static void ResolveCoreDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebillsCoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Webills"));
            });

            services.AddScoped<IAddNewIncomeDataStorage, AddNewIncomeDataStorageWithEf>();
            services.AddScoped<IAddNewExpenseDataStorage, AddNewExpenseDataStorageWithEf>();
            services.AddScoped<IEditIncomeDataStorage, EditIncomeDataStorageWithEf>();
            services.AddScoped<IEditExpenseDataStorage, EditExpenseDataStorageWithEf>();
            services.AddScoped<IRemoveIncomeDataStorage, RemoveIncomeDataStorageWithEf>();
            services.AddScoped<IRemoveExpenseDataStorage, RemoveExpenseDataStorageWithEf>();

            services.AddScoped<IAddNewIncome, AddNewIncome>();
            services.AddScoped<IAddNewExpense, AddNewExpense>();
            services.AddScoped<IEditIncome, EditIncome>();
            services.AddScoped<IEditExpense, EditExpense>();
            services.AddScoped<IRemoveIncome, RemoveIncome>();
            services.AddScoped<IRemoveExpense, RemoveExpense>();
        }

        private static void ResolveProfileDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebillsProfileContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Webills"));
            });
        }

        private static void ResolveSecurityDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebillsSecurityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Webills"));
            });

            services.AddScoped<ILogon, UseCases.Logon.Logon>();
            services.AddScoped<ILogonDataStorage, LogonDataStorageWithEf>();
        }
    }
}