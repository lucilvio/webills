using System;
using Lucilvio.Solo.Webills.FinancialControl.Domain;

namespace Lucilvio.Solo.Webills.FinancialControl.EditExpense
{
    public class EditExpenseInput
    {
        public EditExpenseInput(Guid userId, Guid id, string name, int category, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Id = id;
            this.Name = name;
            this.Category = (Expense.ExpenseCategory)category;
            this.Date = date;
            this.Value = new TransactionValue(value);
        }

        internal Guid UserId { get; }
        internal Guid Id { get; }
        internal string Name { get; }
        internal Expense.ExpenseCategory Category { get; }
        internal DateTime Date { get; }
        internal TransactionValue Value { get; }
    }
}