using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public interface IAddNewExpenseMessage
    {
        Guid UserId { get; }
        string Name { get; }
        string Category { get; }
        DateTime Date { get; }
        decimal Value { get; }
    }
}
