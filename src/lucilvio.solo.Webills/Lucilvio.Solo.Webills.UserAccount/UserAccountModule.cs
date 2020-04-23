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
        public event Action<GeneratedPassword> PasswordGenerated;
        public event Action<CreatedAccount> UserAccountCreated;

        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public UserAccountModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
        }

        public async Task GenerateNewPassword(SendNewPasswordInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            GeneratedPassword generatedPassword = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GenerateNewPasswordComponent>();
                generatedPassword = await component.Execute(input);
            }

            this.PasswordGenerated?.Invoke(generatedPassword);
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

        public async Task CreateNewUserAccount(CreateUserAccountInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            CreatedAccount createdAccount = default;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<CreateUserAccountComponent>();
                createdAccount = await component.Execute(input);
            }

            this.UserAccountCreated?.Invoke(createdAccount);
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }
    }
}