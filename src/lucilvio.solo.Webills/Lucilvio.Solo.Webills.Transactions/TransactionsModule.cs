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
        public event Action<AddedIncome> IncomeAdded;
        public event Action<EditedIncome> IncomeEdited;
        public event Action<RemovedIncome> IncomeRemoved;

        public event Action<AddedExpense> ExpenseAdded;
        public event Action<EditedExpense> ExpenseEdited;
        public event Action<RemovedExpense> ExpenseRemoved;

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

        public async Task AddNewIncome(AddNewIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            AddedIncome addedIncome = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<AddNewIncomeComponent>();
                addedIncome = await component.Execute(input);
            }

            this.IncomeAdded?.Invoke(addedIncome);
        }

        public async Task EditIncome(EditIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            EditedIncome editedIncome = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<EditIncomeComponent>();
                editedIncome = await component.Execute(input);
            }

            this.IncomeEdited?.Invoke(editedIncome);
        }

        public async Task EditExpense(EditExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            EditedExpense editedExpense = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<EditExpenseComponent>();
                editedExpense = await component.Execute(input);
            }

            this.ExpenseEdited?.Invoke(editedExpense);
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

        public async Task AddNewExpense(AddNewExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            AddedExpense addedExpense = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<AddNewExpenseComponent>();
                addedExpense = await component.Execute(input);
            }

            this.ExpenseAdded?.Invoke(addedExpense);
        }

        public async Task RemoveIncome(RemoveIncomeInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            RemovedIncome removedIncome = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<RemoveIncomeComponent>();
                removedIncome = await component.Execute(input);
            }

            this.IncomeRemoved?.Invoke(removedIncome);
        }

        public async Task RemoveExpense(RemoveExpenseInput input)
        {
            if (input == null)
                throw new Error.ComponentInputNotInformed();

            RemovedExpense removedExpense = null;

            using (AsyncScopedLifestyle.BeginScope(this._dependencyResolver.Container))
            {
                var component = this._dependencyResolver.Container.GetInstance<RemoveExpenseComponent>();
                removedExpense = await component.Execute(input);
            }

            this.ExpenseRemoved?.Invoke(removedExpense);
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