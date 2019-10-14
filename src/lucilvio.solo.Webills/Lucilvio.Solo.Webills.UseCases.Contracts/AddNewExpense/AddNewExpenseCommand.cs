using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense
{
    public abstract class AddNewExpenseCommand
    {
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}