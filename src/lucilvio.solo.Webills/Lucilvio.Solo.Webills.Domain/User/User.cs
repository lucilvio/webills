using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class User
    {
        private readonly List<Income> _incomes;
        private readonly List<Expense> _expenses;

        public User(string name)
        {
            this.Name = name;
            this._incomes = new List<Income>();
            this._expenses = new List<Expense>();
        }

        public string Name { get; }
        public IEnumerable<Income> Incomes => this._incomes;
        public IEnumerable<Expense> Expenses => this._expenses;


        public bool HasIncomes => this._incomes.Any();
        public bool HasExpenses => this._expenses.Any();
        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.Expenses.Sum(e => e.Value.Value);

        public void AddIncome(Income income)
        {
            if (income == null)
                throw new UserCannotAddNullIncome();

            this._incomes.Add(income);
        }

        public void AddExpense(Expense expense)
        {
            if (expense == null)
                throw new UserCannotAddNullExpense();

            this._expenses.Add(expense);
        }
    }
}