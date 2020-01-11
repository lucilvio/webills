using System;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense
{
    public abstract class RemoveExpenseCommand
    {
        protected RemoveExpenseCommand(Guid userId, Guid expenseId)
        {
            UserId = userId;
            ExpenseId = expenseId;
        }

        public Guid UserId { get; }
        public Guid ExpenseId { get; }
    }
}