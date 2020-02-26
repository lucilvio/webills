using System;

namespace Lucilvio.Solo.Webills.Transactions.RemoveExpense
{
    public interface IRemoveIncomeInput
    {
        public Guid UserId { get; }
        public Guid IncomeId { get; }
    }
}