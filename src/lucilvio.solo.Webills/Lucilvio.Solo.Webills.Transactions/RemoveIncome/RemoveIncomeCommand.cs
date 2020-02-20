using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public abstract class RemoveIncomeCommand
    {
        public Guid UserId { get; protected set; }
        public Guid IncomeId { get; protected set; }
    }
}