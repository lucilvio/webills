using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public class RemoveIncomeInput
    {
        public RemoveIncomeInput(Guid userId, Guid id)
        {
            this.UserId = userId;
            this.Id = id;
        }

        public Guid UserId { get; }
        public Guid Id { get; }
    }
}