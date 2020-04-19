using System;
using Lucilvio.Solo.Webills.Transactions.Domain;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public class AddedExpense
    {
        internal AddedExpense(User user, Expense expense)
        {
            if(user == null || expense == null)
                return;

            this.UserId = user.Id;
            this.Id = expense.Id;
            this.Name = expense.Name;
            this.Date = expense.Date;
            this.Value = expense.Value.Value;
            this.Category = (int)expense.Category;
            this.CategoryName = expense.ToString();
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