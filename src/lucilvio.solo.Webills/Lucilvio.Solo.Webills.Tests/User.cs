using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lucilvio.Solo.Webills.Tests
{
    public class User
    {
        private readonly List<Income> _incomes;
        private readonly List<Expense> _expenses;

        public User()
        {
            this._incomes = new List<Income>();
            this._expenses = new List<Expense>();
        }

        public ReadOnlyCollection<Income> Incomes => this._incomes.AsReadOnly();

        internal void AddExpense(Expense expense)
        {
            if (expense == null)
                throw new UserCannotAddNullExpense();

            this._expenses.Add(expense);
        }

        public ReadOnlyCollection<Expense> Expenses => this._expenses.AsReadOnly();

        public bool HasIncomes => this._incomes.Any();
        public bool HasExpenses => this._expenses.Any();

        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.Expenses.Sum(e => e.Value.Value);

        public void AddIncome(Income income)
        {
            if (income == null)
                throw new UserCannotAddNullIncome();

            this._incomes.Add(income);
        }
    }
}