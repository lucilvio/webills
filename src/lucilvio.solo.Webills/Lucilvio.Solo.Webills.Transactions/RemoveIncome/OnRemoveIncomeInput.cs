using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveIncome
{
    public class OnRemoveIncomeInput
    {
        public OnRemoveIncomeInput(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
    }
}