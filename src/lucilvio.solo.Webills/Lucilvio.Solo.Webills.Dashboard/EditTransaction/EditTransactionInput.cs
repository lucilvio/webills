using System;

namespace Lucilvio.Solo.Webills.Dashboard.EditTransaction
{
    public class EditTransactionInput
    {
        private EditTransactionInput(Guid userId, Guid id, string name, DateTime date, decimal value, int? category)
        {
            this.Id = id;
            this.UserId = userId;
            this.Name = name;
            this.Date = date;
            this.Value = value;
            this.Category = category;
        }

        public static EditTransactionInput Income(Guid userId, Guid id, string name, DateTime date,
            decimal value) => new EditTransactionInput(userId, id, name, date, value, null);

        public static EditTransactionInput Expense(Guid userId, Guid id, string name, int category,
            DateTime date, decimal value) => new EditTransactionInput(userId, id, name, date, value, category);

        internal Guid Id { get; }
        internal Guid UserId { get; }
        internal string Name { get; }
        internal DateTime Date { get; }
        internal decimal Value { get; }
        internal int? Category { get; }
    }
}