using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;

namespace Lucilvio.Solo.Webills.Tests.UseCases.AddNewExpense
{
    public class AddNewExpenseCommandStub : AddNewExpenseCommand
    {
        public AddNewExpenseCommandStub(string name, DateTime date, TransactionValue value)
        {
            base.Name = name;
            base.Date = date;
            base.Value = value;
        }
    }
}