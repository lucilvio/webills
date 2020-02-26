using System;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public class NewAddedExpense
    {
        internal NewAddedExpense(Guid userId, Expense newExpense)
        {
            this.UserId = userId;
            this.Id = newExpense.Id;
            this.Name = newExpense.Name;
            this.Date = newExpense.Date;
            this.Value = newExpense.Value.Value;
            this.Category = (int)newExpense.Category;
            this.CategoryName = newExpense.ToString();
        }

        public Guid Id { get; internal set; }
        public Guid UserId { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public decimal Value { get; internal set; }
        public int Category { get; internal set; }
        public string CategoryName { get; internal set; }
    }
}