using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    public class RemovedIncome
    {
        public RemovedIncome(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
    }
}