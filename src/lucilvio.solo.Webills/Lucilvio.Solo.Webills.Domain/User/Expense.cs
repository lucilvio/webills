using System;
using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public partial class Expense
    {
        public Expense(string name, Category category, DateTime date, TransactionValue value)
        {
            if (string.IsNullOrEmpty(name))
                throw new ExpenseMustHaveName();

            if (date < DateTime.Now.AddYears(-100))
                throw new ExpenseCannotBeOlderThanOneHundredYears();

            if (value == null)
                throw new ExpenseTransactionValueCannotBeNull();

            this.Name = name;
            this.Date = date;
            this.Category = category;
            this.Value = value;

            this.Number = Guid.NewGuid();
        }

        public Guid Number { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public Category Category { get; }
        public TransactionValue Value { get; }
    }
}