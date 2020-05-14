using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public class OnRemovedExpenseInput
    {
        public OnRemovedExpenseInput(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
    }
}