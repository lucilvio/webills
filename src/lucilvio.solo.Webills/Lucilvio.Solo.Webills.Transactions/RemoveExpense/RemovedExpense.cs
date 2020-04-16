using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public class RemovedExpense
    {
        public RemovedExpense(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
    }
}