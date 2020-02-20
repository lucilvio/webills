using System;

namespace Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewExpense
{
    public abstract class AddNewExpenseCommand
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}