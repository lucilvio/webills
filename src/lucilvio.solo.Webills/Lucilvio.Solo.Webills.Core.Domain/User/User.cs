using System;
using System.Linq;
using System.Collections.Generic;

using Lucilvio.Solo.Webills.Core.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Core.Domain.User
{
    public class User
    {
        private readonly List<Income> _incomes;
        private readonly List<Expense> _expenses;

        private User()
        {
            this._incomes = new List<Income>();
            this._expenses = new List<Expense>();
        }

        public User(string name) : this()
        {
            this.Name = name;
        }

        public Guid Id { get; private set;  }
        public string Name { get; private set; }

        public IEnumerable<Income> Incomes => this._incomes;
        public IEnumerable<Expense> Expenses => this._expenses;

        public decimal Balance => this.Incomes.Sum(i => i.Value.Value) - this.Expenses.Sum(e => e.Value.Value);

        public Guid AddIncome(string name, DateTime date, TransactionValue value)
        {
            var income = new Income(name, date, value);
            this._incomes.Add(income);

            return income.Id;
        }

        public Guid AddExpense(string name, Category category, DateTime date, TransactionValue value)
        {
            var newExpense = new Expense(name, category, date, value);
            this._expenses.Add(newExpense);

            return newExpense.Id;
        }

        public void AddFixedExpense(string name, Category category, DateTime date, TransactionValue transactionValue, Recurrency recurrency, DateTime until)
        {
            this._expenses.Add(new FixedExpense(name, category, date, transactionValue, recurrency, until));

            if (recurrency == Recurrency.Daily || recurrency == Recurrency.Weekly || recurrency == Recurrency.Biweekly)
            {
                for (DateTime i = date.AddDays((double)recurrency); i <= until; i = i.AddDays((double)recurrency))
                {
                    var newFixedExpense = new FixedExpense(name, category, i, transactionValue, recurrency, until);
                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Monthly)
            {
                var months = Math.Abs(12 * (until.Year - date.Year) + until.Month - date.Month);

                for (int i = 1; i <= months; i += 1)
                {
                    var newDate = date.AddMonths(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, newDate.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Bimonthly)
            {
                var months = Math.Abs(12 * (until.Year - date.Year) + until.Month - date.Month);

                for (int i = 2; i <= months; i += 2)
                {
                    var newDate = date.AddMonths(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, newDate.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Trimonthly)
            {
                var months = Math.Abs(12 * (until.Year - date.Year) + until.Month - date.Month);

                for (int i = 3; i <= months; i += 3)
                {
                    var newDate = date.AddMonths(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, newDate.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Quarterly)
            {
                var months = Math.Abs(12 * (until.Year - date.Year) + until.Month - date.Month);

                for (int i = 4; i <= months; i += 4)
                {
                    var newDate = date.AddMonths(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, newDate.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Semiannualy)
            {
                var months = Math.Abs(12 * (until.Year - date.Year) + until.Month - date.Month);

                for (int i = 6; i <= months; i += 6)
                {
                    var newDate = date.AddMonths(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, newDate.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }
            else if (recurrency == Recurrency.Annual)
            {
                var years = until.Year - date.Year;

                for (int i = 1; i <= years; i++)
                {
                    var newDate = date.AddYears(i);
                    var day = date.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < date.Day)
                        day = newDate.Day;

                    var newFixedExpense = new FixedExpense(name, category, new DateTime(newDate.Year, date.Month, day), transactionValue,
                        recurrency, until);

                    this._expenses.Add(newFixedExpense);
                }
            }

        }

        public void AlterIncome(Guid incomeNumber, string name, DateTime date, TransactionValue value)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Id == incomeNumber);
            var foundIncomeIndex = this._incomes.IndexOf(foundIncome);

            if (foundIncomeIndex < 0)
                throw new IncomeNotFound();

            this._incomes[foundIncomeIndex] = new Income(name, date, value);
        }

        public void EditExpense(Guid expenseId, string name, Category category, DateTime date, TransactionValue value)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Id == expenseId);
            var foundExpenseIndex = this._expenses.IndexOf(foundExpense);

            if (foundExpenseIndex < 0)
                throw new ExpenseNotFound();

            this._expenses[foundExpenseIndex] = new Expense(name, category, date, value);
        }

        public void EditFixedExpense(Guid expenseId, string name, Category category, DateTime date, TransactionValue value, DateTime until)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Id == expenseId);
            var foundExpenseIndex = this._expenses.IndexOf(foundExpense);

            if (foundExpenseIndex < 0)
                throw new ExpenseNotFound();

            this._expenses[foundExpenseIndex] = new Expense(name, category, date, value);
        }

        public void RemoveExpense(Guid expenseNumber)
        {
            var foundExpense = this._expenses.FirstOrDefault(e => e.Id == expenseNumber);

            if (foundExpense == null)
                throw new ExpenseNotFound();

            this._expenses.Remove(foundExpense);
        }

        public void RemoveIncome(Guid incomeNumber)
        {
            var foundIncome = this._incomes.FirstOrDefault(i => i.Id == incomeNumber);

            if (foundIncome == null)
                throw new IncomeNotFound();

            this._incomes.Remove(foundIncome);
        }
    }
}