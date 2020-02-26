using System;
using System.Reflection;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Dashboard.AddExpense;
using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Dashboard
{
    public class DashboardModule
    {
        private readonly Container _container;

        public DashboardModule()
        {
            this._container = new Container();
            this.ResolveModuleDependencies();
        }

        public async Task AddExpense(IAddExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<AddExpenseComponent>();
                await component.Execute(input);
            }
        }

        private void ResolveModuleDependencies()
        {
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.Register<DashBoardContext>(Lifestyle.Scoped);

            var currentAssembly = Assembly.GetExecutingAssembly();
            this._container.Collection.Register(typeof(IComponent), currentAssembly);

            _container.Verify();
        }

        internal class Error
        {
            public class ComponentInputNotInformed : Exception { }
        }
    }
}
