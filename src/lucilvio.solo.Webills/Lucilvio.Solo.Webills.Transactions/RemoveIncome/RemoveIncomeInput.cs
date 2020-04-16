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

        internal Guid UserId { get; }
        internal Guid Id { get; }
    }
}