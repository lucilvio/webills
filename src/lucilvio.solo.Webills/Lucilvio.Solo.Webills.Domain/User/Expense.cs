using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;
using System;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class Expense
    {
        public Expense(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;

            if (value == null)
                throw new ExpenseTransactionValueCannotBeNull();

            this.Value = value;

            this.Number = Guid.NewGuid();
        }

        public Guid Number { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
    }
}