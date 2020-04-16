using System;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public class AddNewExpenseInput
    {
        public AddNewExpenseInput(Guid userId, string name, int category, DateTime date, decimal value)
        {
            this.Name = name;
            this.Date = date;
            this.UserId = userId;
            this.Category = (Category)category;
            this.Value = new TransactionValue(value);
        }

        internal Guid UserId { get; }
        internal string Name { get; }
        internal Category Category { get; }
        internal DateTime Date { get; }
        internal TransactionValue Value { get; }
    }
}