using System;

namespace Lucilvio.Solo.Webills.FinancialControl.RemoveExpense
{
    public class OnRemovedExpenseInput
    {
        public OnRemovedExpenseInput(Guid userId, Guid id)
        {
            this.Id = id;
            this.UserId = userId;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
    }
}