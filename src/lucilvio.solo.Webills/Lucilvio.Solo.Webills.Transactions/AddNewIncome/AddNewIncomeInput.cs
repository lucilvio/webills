using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public interface IAddNewIncomeInput
    {
        public Guid UserId { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}