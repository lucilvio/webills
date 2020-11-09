using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewIncome
{
    public interface IAddNewIncomeMessage
    {
        Guid UserId { get; }
        string Name { get; }
        DateTime Date { get; }
        string Category { get; }
        decimal Value { get; }
    }
}