using System;
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
        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.TotalExpenses;

        public decimal TotalIncomes => this.Incomes.Sum(i => i.Value.Value);
        public decimal TotalExpenses => this.Expenses.Sum(e => e.Value.Value);

        public Guid AddIncome(string name, DateTime date, TransactionValue value)
        {
            var income = new Income(name, date, value);
            this._incomes.Add(income);

            return income.Number;
        }

        public Guid AddExpense(string name, DateTime date, TransactionValue value)
        {
            var newExpense = new Expense(name, date, value);
            this._expenses.Add(newExpense);

            return newExpense.Number;
        }

        public void AlterIncome(Guid incomeNumber, string name, DateTime date, TransactionValue value)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Number == incomeNumber);
            var foundIncomeIndex = this._incomes.IndexOf(foundIncome);

            if (foundIncomeIndex < 0)
                throw new IncomeNotFound();

            this._incomes[foundIncomeIndex] = new Income(name, date, value);
        }

        public void AlterExpense(Guid expenseNumber, string name, DateTime date, TransactionValue value)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Number == expenseNumber);
            var foundExpenseIndex = this._expenses.IndexOf(foundExpense);

            if (foundExpenseIndex < 0)
                throw new ExpenseNotFound();

            this._expenses[foundExpenseIndex] = new Expense(name, date, value);
        }

        public void RemoveExpense(Guid expenseNumber)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Number == expenseNumber);

            if (foundExpense == null)
                throw new ExpenseNotFound();

            this._expenses.Remove(foundExpense);
        }

        public void RemoveIncome(Guid incomeNumber)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Number == incomeNumber);

            if (foundIncome == null)
                throw new IncomeNotFound();

            this._incomes.Remove(foundIncome);
        }
    }
}