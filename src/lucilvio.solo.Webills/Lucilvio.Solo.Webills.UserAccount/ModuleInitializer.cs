using Lucilvio.Solo.Architecture;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddUserAccountModule(this IServiceCollection services,
            Module.Configurations configurations)
        {
            services.AddSingleton<Module>(provider => new UserAccountModule(configurations));

            return services;
        }
    }
}