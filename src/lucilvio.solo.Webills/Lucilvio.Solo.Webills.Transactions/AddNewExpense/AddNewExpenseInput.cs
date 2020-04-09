using System;

namespace Lucilvio.Solo.Webills.Transactions.AddNewExpense
{
    public class AddNewExpenseInput
    {
        public AddNewExpenseInput(Guid userId, string name, string category, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
        }

        public Guid UserId { get; }
        public string Name { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}