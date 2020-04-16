using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.CreateUser;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.GetExpense;
using Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;
using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;

using SimpleInjector.Lifestyles;

namespace Lucilvio.Solo.Webills.Transactions
{
    public class TransactionsModule
    {
        private readonly DependencyResolverWithSimpleInjector _dependencyResolver;

        public TransactionsModule()
        {
            this._dependencyResolver = new DependencyResolverWithSimpleInjector();
        }

        public async Task CreateUser(CreateUserInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<CreateUserComponent>();
                await component.Execute(input);
            }
        }

        public async Task<GetIncomeByIdOutput> GetIncomeById(GetIncomeByIdInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GetIncomeByIdComponent>();
                return await component.Execute(input);
            }
        }

        public async Task<GetExpensesByFilterOutput> GetExpensesByFilter(GetExpensesByFilterInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GetExpensesByFilterComponent>();
                return await component.Execute(input);
            }
        }

        public async Task AddNewIncome(AddNewIncomeInput input, Func<CreatedIncome, Task> onIncomeCreate = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<AddNewIncomeComponent>();
                await component.Execute(input, onIncomeCreate);
            }
        }

        public async Task EditIncome(EditIncomeInput input, Func<EditedIncome, Task> onEditIncome = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<EditIncomeComponent>();
                await component.Execute(input, onEditIncome);
            }
        }

        public async Task EditExpense(EditExpenseInput input, Func<EditedExpense, Task> onEditExpense = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<EditExpenseComponent>();
                await component.Execute(input, onEditExpense);
            }
        }

        public async Task<GetExpenseByIdOutput> GetExpenseById(GetExpenseByIdInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GetExpenseByIdComponent>();
                return await component.Execute(input);
            }
        }

        public async Task AddNewExpense(AddNewExpenseInput input, Func<CreatedExpense, Task> onExpenseCreate = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<AddNewExpenseComponent>();
                await component.Execute(input, onExpenseCreate);
            }
        }

        public async Task RemoveIncome(RemoveIncomeInput input, Func<RemovedIncome, Task> onRemoveIncome = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<RemoveIncomeComponent>();
                await component.Execute(input, onRemoveIncome);
            }
        }

        public async Task RemoveExpense(RemoveExpenseInput input, Func<RemovedExpense, Task> onRemoveExpense = null)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<RemoveExpenseComponent>();
                await component.Execute(input, onRemoveExpense);
            }
        }

        public async Task<GetIncomesByFilterOutput> GetIncomesByFilter(GetIncomesByFilterInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<GetIncomesByFilterComponent>();
                return await component.Execute(input);
            }
        }

        public class Error
        {
            public class ComponentInputNotInformed : Exception { }
            public class QueryNotInformed : Exception { }
        }
    }
}