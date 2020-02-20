using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public abstract class AddNewExpenseCommand
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Value { get; protected set; }
    }
}