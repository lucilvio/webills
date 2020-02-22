using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public abstract class RemoveIncomeCommand : ICommand
    {
        public Guid UserId { get; protected set; }
        public Guid IncomeId { get; protected set; }
    }
}