using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public interface IAddNewExpenseInput
    {
        public Guid UserId { get; }
        public string Name { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}