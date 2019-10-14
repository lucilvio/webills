using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome
{
    public abstract class EditIncomeCommand
    {
        public Guid Number { get; protected set; }
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}