using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense
{
    public abstract class EditExpenseCommand
    {
        public Guid Id { get; set; }
        public string Name { get; protected set; }
        public Category Category { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}