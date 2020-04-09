using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.GenerateNewPassword;
using Lucilvio.Solo.Webills.UserAccount.Login;

using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule
    {
        readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public UserAccountModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
        }

        public async Task GenerateNewPassword(SendNewPasswordInput input, Func<GeneratedPassword, Task> onPasswordGenerate)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GenerateNewPasswordComponent>();
                await component.Execute(input, onPasswordGenerate);
            }
        }

        public async Task Login(LoginInput input, Func<LoggedUser, Task> onLogin)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<LoginComponent>();
                await component.Execute(input, onLogin);
            }
        }

        public async Task CreateNewUserAccount(CreateUserAccountInput input, Func<UserAccountCreated, Task> onCreate)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<CreateUserAccountComponent>();
                await component.Execute(input, onCreate);
            }
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }
    }
}