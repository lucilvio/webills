using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;
using System;

namespace Lucilvio.Solo.Webills.Tests.UseCases.EditExpenses
{
    internal class EditExpenseCommandStub : EditExpenseCommand
    {
        public EditExpenseCommandStub(Guid expenseNumber, string name, Category category, DateTime date, TransactionValue value)
        {
            base.Id = expenseNumber;
            base.Name = name;
            base.Category = category;
            base.Date = date;
            base.Value = value;
        }
    }
}