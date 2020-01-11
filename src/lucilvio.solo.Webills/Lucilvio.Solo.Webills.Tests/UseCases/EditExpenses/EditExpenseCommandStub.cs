using System;

using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditExpense;

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