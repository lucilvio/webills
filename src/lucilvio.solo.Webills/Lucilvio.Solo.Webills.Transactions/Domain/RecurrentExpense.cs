using System;
using System.Linq;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal class RecurrentExpense
    {
        private readonly List<Expense> _expenses;

        private RecurrentExpense()
        {
            this.Id = Guid.NewGuid();
            this._expenses = new List<Expense>();
        }

        internal RecurrentExpense(string name, Expense.ExpenseCategory category, DateTime date, TransactionValue value,
            Recurrency recurrency) : this()
        {
            this.Recurrency = recurrency;

            this._expenses.Add(Expense.NewWithRecurrency(name, category, date, value, this));

            foreach (var nextDate in recurrency.DatesUntilRecurrencyEndsByFrequency(date))
            {
                this._expenses.Add(Expense.NewWithRecurrency(name, category, nextDate, value, this));
            }
        }

        public Guid Id { get; }
        public Recurrency Recurrency { get; }
        public IEnumerable<Expense> Expenses => this._expenses;

        public int ExpensesCount => this.Expenses.Count();
    }
}