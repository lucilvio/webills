using System;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    public abstract class EditExpenseCommand : ICommand
    {
        public Guid UserId { get; protected set; }
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Value { get; protected set; }
    }
}