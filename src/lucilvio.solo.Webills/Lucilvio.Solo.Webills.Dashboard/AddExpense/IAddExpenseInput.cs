using System;

namespace Lucilvio.Solo.Webills.Dashboard.AddExpense
{
    public interface IAddExpenseInput
    {
        public Guid UserId { get; }
        public Guid TransactionId { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
        public int Category { get; }
        public string CategoryName { get; }
    }
}
