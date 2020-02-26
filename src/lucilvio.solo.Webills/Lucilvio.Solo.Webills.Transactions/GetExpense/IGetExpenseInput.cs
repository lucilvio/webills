using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    public interface IGetExpenseInput
    {
        public Guid UserId { get; }
        public Guid Id { get; }
    }
}