using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome
{
    public abstract class AddNewIncomeCommand
    {
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public TransactionValue Value { get; protected set; }
    }
}