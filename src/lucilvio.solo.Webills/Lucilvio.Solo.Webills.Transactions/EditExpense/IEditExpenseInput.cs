using System;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    public interface IEditExpenseInput
    {
        public Guid UserId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}