using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.UserAccount;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class DependenciesResolvers
    {
        public static void AddObsoleteModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(svc => new WebillsReadContext(configuration.GetConnectionString("Webills")));

            services.AddScoped<IUserDashboardQueryHandler, UserTransactionsInformationQuery>();

            services.AddScoped<IUserDashboardQueryHandler, UserTransactionsInformationQuery>();
            services.AddScoped<IGetUserIncomesByFilterQueryHandler, GetUserIncomesByFilterQueryHandler>();
            services.AddScoped<IGetUserExpensesByFilterQueryHandler, GetUserExpensesByFilterQueryHandler>();
        }

        public static void AddTransactionsModule(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(services => new TransactionsModule());

        public static void AddUserAccoutModule(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(services => new UserAccountModule());
    }
}