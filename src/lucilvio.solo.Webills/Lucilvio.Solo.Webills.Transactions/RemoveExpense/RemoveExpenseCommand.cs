using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public abstract class RemoveExpenseCommand
    {
        public Guid UserId { get; protected set; }
        public Guid ExpenseId { get; protected set; }
    }
}