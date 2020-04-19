using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public class RemoveExpenseInput
    {
        public RemoveExpenseInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        internal Guid UserId { get; }
        internal Guid Id { get; }
    }
}