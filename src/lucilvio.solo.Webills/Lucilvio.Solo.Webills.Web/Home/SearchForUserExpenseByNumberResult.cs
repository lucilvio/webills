using System;
using Lucilvio.Solo.Webills.Domain.User;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class SearchForUserExpenseByNumberResult
    {
        public SearchForUserExpenseByNumberResult(Expense expense)
        {
            if (expense == null)
                return;

            this.Number = expense.Number;
            this.Name = expense.Name;
            this.Date = expense.Date;
            this.Value = expense.Value;
            this.Category = expense.Category;
        }

        public Guid Number { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
        public Category Category { get; }
    }
}