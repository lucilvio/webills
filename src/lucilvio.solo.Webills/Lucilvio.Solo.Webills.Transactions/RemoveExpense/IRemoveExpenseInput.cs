using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public interface IRemoveExpenseInput
    {
        public Guid UserId { get; }
        public Guid ExpenseId { get; }
    }
}