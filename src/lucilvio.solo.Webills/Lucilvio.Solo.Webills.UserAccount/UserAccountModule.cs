using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.UserAccount.CreateUserAccount;
using Lucilvio.Solo.Webills.UserAccount.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.UserAccount.Login;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.UserAccount
{
    public class UserAccountModule
    {
        private readonly Container _container;

        public UserAccountModule()
        {
            this._container = new Container();
            this.ResolveModuleDependencies();
        }

        public async Task Login(LoginInput input, Func<LoggedUser, Task> onLogin)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<LoginComponent>();
                await component.Execute(input, onLogin);
            }
        }

        public async Task CreateNewUserAccount(ICreateUserAccountInput input, Func<UserAccountCreated, Task> onCreate)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<CreateUserAccountComponent>();
                await component.Execute(input, onCreate);
            }
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }

        private void ResolveModuleDependencies()
        {
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.Register<UserAccountContext>(Lifestyle.Scoped);

            var currentAssembly = Assembly.GetExecutingAssembly();

            var dataAccessTypes = currentAssembly.GetTypes().Where(t => t.Name.Contains("DataAccess"));
            var c = dataAccessTypes.Where(t => !t.IsInterface);

            foreach (var dataAccessType in dataAccessTypes.Where(t => t.IsInterface))
            {
                var concreteType = c.Where(t => dataAccessType.IsAssignableFrom(t)).FirstOrDefault();

                if (concreteType != null)
                    this._container.Register(dataAccessType, concreteType);
            }

            this._container.Collection.Register(typeof(IComponent), currentAssembly);

            this._container.Verify();
        }
    }
}