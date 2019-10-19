using System;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense
{
    public abstract class RemoveExpenseCommand
    {
        public Guid ExpenseNumber { get; protected set; }
    }
}