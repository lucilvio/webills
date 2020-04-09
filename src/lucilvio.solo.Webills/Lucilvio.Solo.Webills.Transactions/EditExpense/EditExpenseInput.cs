using System;

namespace Lucilvio.Solo.Webills.Transactions.EditExpense
{
    public class EditExpenseInput
    {
        public EditExpenseInput(Guid userId, Guid id, string name, string category, DateTime date, decimal value)
        {
            this.UserId = userId;
            this.Id = id;
            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
        }

        public Guid UserId { get; }
        public Guid Id { get; }
        public string Name { get; }
        public string Category { get; }
        public DateTime Date { get; }
        public decimal Value { get; }
    }
}