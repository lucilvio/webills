using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public class RemoveExpenseInput
    {
        public RemoveExpenseInput(Guid userId, Guid expenseId)
        {
            this.UserId = userId;
            this.ExpenseId = expenseId;
        }

        public Guid UserId { get; }
        public Guid ExpenseId { get; }
    }
}