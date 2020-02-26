using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.GetExpense;
using Lucilvio.Solo.Webills.Transactions.GetIncome;
using Lucilvio.Solo.Webills.Transactions.Infraestructure.DataAccess;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;

using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        private readonly Container _container;

        public TransactionsModule()
        {
            this._container = new Container();
            this.ResolveModuleDependencies();
        }

        public async Task CreateUser(ICreateUserInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<CreateUserComponent>();
                await component.Execute(input);
            }
        }

        public async Task AddNewIncome(IAddNewIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<AddNewIncomeComponent>();
                await component.Execute(input);
            }
        }


        public async Task<GetIncomeOutput> GetIncome(IGetIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<GetIncomeComponent>();
                return await component.Execute(input);
            }
        }

        public async Task EditIncome(IEditIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<EditIncomeComponent>();
                await component.Execute(input);
            }
        }

        public async Task EditExpense(IEditExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<EditExpenseComponent>();
                await component.Execute(input);
            }
        }

        public async Task<GetExpenseOutput> GetExpense(IGetExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<GetExpenseComponent>();
                return await component.Execute(input);
            }
        }

        public async Task AddNewExpense(IAddNewExpenseInput input, Func<NewAddedExpense, Task> onAddExpense)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<AddNewExpenseComponent>();
                await component.Execute(input, onAddExpense);
            }
        }

        public async Task RemoveIncome(IRemoveIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<RemoveIncomeComponent>();
                await component.Execute(input);
            }
        }

        public async Task RemoveExpense(IRemoveExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._container))
            {
                var component = this._container.GetInstance<RemoveExpenseComponent>();
                await component.Execute(input);
            }
        }

        public class Error
        {
            public class ComponentInputNotInformed : Exception { }
            public class QueryNotInformed : Exception { }
        }

        private void ResolveModuleDependencies()
        {
            this._container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            this._container.Register<TransactionsContext>(Lifestyle.Scoped);

            var currentAssembly = Assembly.GetExecutingAssembly();

            var dataAccessTypes = currentAssembly.GetTypes().Where(t => t.Name.Contains("DataAccess"));
            var c = dataAccessTypes.Where(t => !t.IsInterface);

            foreach (var dataAccessType in dataAccessTypes.Where(t => t.IsInterface))
            {
                var concreteType = c.Where(t => dataAccessType.IsAssignableFrom(t)).FirstOrDefault();

                if (concreteType != null)
                    _container.Register(dataAccessType, concreteType);
            }

            this._container.Collection.Register(typeof(IComponent), currentAssembly);

            _container.Verify();
        }
    }
}