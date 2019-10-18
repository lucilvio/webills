using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System;

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
        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.TotalExpenses;

        public decimal TotalIncomes => this.Incomes.Sum(i => i.Value.Value);
        public decimal TotalExpenses => this.Expenses.Sum(e => e.Value.Value);

        public Guid AddIncome(Income income)
        {
            if (income == null)
                throw new UserCannotAddNullIncome();

            this._incomes.Add(income);

            return income.Number;
        }

        public Guid AddExpense(Expense expense)
        {
            if (expense == null)
                throw new UserCannotAddNullExpense();

            this._expenses.Add(expense);

            return expense.Number;
        }

        public void AlterIncome(Guid incomeNumber, Income income)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Number == incomeNumber);
            var foundIncomeIndex = this._incomes.IndexOf(foundIncome);

            if (foundIncomeIndex < 0)
                throw new IncomeNotFound();

            this._incomes[foundIncomeIndex] = income;
        }

        public void AlterExpense(Guid expenseNumber, Expense expense)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Number == expenseNumber);
            var foundExpenseIndex = this._expenses.IndexOf(foundExpense);

            if (foundExpenseIndex < 0)
                throw new ExpenseNotFound();

            this._expenses[foundExpenseIndex] = expense;
        }
    }
}