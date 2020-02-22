using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public abstract class AddNewIncomeCommand : ICommand
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public DateTime Date { get; protected set; }
        public decimal Value { get; protected set; }
    }
}