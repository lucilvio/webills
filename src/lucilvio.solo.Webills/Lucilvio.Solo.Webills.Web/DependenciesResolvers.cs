using Lucilvio.Solo.Webills.Dashboard;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.UserAccount;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.Web
{
    public static class DependenciesResolvers
    {
        public static void AddModules(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<DashboardModule>();
            services.AddSingleton<UserAccountModule>();
            services.AddSingleton<TransactionsModule>();
        }
    }
}