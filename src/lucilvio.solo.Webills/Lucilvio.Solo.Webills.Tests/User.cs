using System.Linq;
using System.Collections.Generic;

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

        public IEnumerable<Income> Incomes => this._incomes;
        public IEnumerable<Expense> Expenses => this._expenses;

        public void AddExpense(Expense expense)
        {
            if (expense == null)
                throw new UserCannotAddNullExpense();

            this._expenses.Add(expense);
        }

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