using Lucilvio.Solo.Architecture;
using Lucilvio.Solo.Architecture.Modules.AutofacModule;
using Microsoft.Extensions.DependencyInjection;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public static class ModuleInitializer
    {
        public static IServiceCollection AddUserAccountModule(this IServiceCollection services,
            Configurations configurations)
        {
            services.AddSingleton<IModuleResolver<IUserAccountModule>>(provider
                => new AutofacModuleResolver<IUserAccountModule>(configurations));

            services.AddSingleton<IUserAccountModule>(provider =>
                new UserAccountModule(provider.GetService<IModuleResolver<IUserAccountModule>>()));

            return services;
        }
    }
}