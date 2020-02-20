using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.UserAccount;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class DependenciesResolver
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            ResolveTransactionsComponentDependencies(services, configuration);
            ResolveUserAccountComponentDependencies(services, configuration);
        }

        private static void ResolveTransactionsComponentDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(services => new TransactionsModule(configuration.GetConnectionString("Webills")));

            services.AddScoped<IAddNewExpenseUseCase>(services => services.GetService<TransactionsModule>());
            services.AddScoped<IEditExpenseUseCase>(services => services.GetService<TransactionsModule>());
            services.AddScoped<IRemoveExpenseUseCase>(services => services.GetService<TransactionsModule>());
            
            services.AddScoped<IAddNewIncomeUseCase>(services => services.GetService<TransactionsModule>());
            services.AddScoped<IEditIncomeUseCase>(services => services.GetService<TransactionsModule>());
            services.AddScoped<IRemoveIncomeUseCase>(services => services.GetService<TransactionsModule>());
        }

        private static void ResolveUserAccountComponentDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(services => new UserAccountModule(configuration.GetConnectionString("Webills")));
            
            services.AddScoped<ILoginUseCase>(services => services.GetService<UserAccountModule>());
            services.AddScoped<ICreateUserAccountUseCase>(services => services.GetService<UserAccountModule>());
        }
    }
}