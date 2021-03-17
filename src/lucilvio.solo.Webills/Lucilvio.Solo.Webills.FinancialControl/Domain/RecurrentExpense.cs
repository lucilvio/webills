using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal class RecurrentExpense
    {
        private readonly List<Expense> _expenses;

        private RecurrentExpense()
        {
            this.Id = Guid.NewGuid();
            this._expenses = new List<Expense>();
        }

        public RecurrentExpense(Guid userId, string name, string category, DateTime date, TransactionValue value,
            DateTime until, int frequency) : this()
        {
            this.Recurrency = new Recurrency(Frequency.FromValue(frequency), until);

            this._expenses.Add(new Expense(userId, name, category, date, value, this.Id));

            foreach (var nextDate in this.Recurrency.DatesUntilRecurrencyEndsByFrequency(date))
            {
                this._expenses.Add(new Expense(userId, name, category, nextDate, value, this.Id));
            }
        }

        public Guid Id { get; }
        public Recurrency Recurrency { get; }
        public IEnumerable<Expense> Expenses => this._expenses;

        public int ExpensesCount => this.Expenses.Count();
    }
}