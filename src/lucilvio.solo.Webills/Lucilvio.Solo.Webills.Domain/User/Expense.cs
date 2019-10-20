using System;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class Expense
    {
        internal Expense(string name, Category category, DateTime date, TransactionValue value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ExpenseMustHaveName();

            this.Name = name;
            this.Date = date;
            this.Category = category;

            if (value == null)
                throw new ExpenseTransactionValueCannotBeNull();

            this.Value = value;

            this.Number = Guid.NewGuid();
        }

        public Guid Number { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public Category Category { get; set; }
        public TransactionValue Value { get; }
    }
}