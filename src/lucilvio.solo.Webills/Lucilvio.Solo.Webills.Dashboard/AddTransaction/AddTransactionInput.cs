using System;

namespace Lucilvio.Solo.Webills.Dashboard.AddExpense
{
    public class AddTransactionInput
    {
        private AddTransactionInput(Guid userId, Guid id, string name, DateTime date, decimal value, 
            int? category, bool isExpense, bool isIncome)
        {
            this.Id = id;
            this.Name = name;
            this.Date = date;
            this.Value = value;
            this.UserId = userId;
            this.Category = category;
            this.IsIncome = isIncome;
            this.IsExpense = isExpense;
        }

        public static AddTransactionInput Income(Guid userId, Guid id, string name, DateTime date, decimal value) 
            => new AddTransactionInput(userId, id, name, date, value, null, false, true);

        public static AddTransactionInput Expense(Guid userId, Guid id, string name, DateTime date, int category, string categoryName, 
            decimal value) => new AddTransactionInput(userId, id, name, date, value, category, true, false);

        internal Guid UserId { get; }
        internal Guid Id { get; }
        internal string Name { get; }
        internal DateTime Date { get; }
        internal decimal Value { get; }
        internal int? Category { get; }
        internal bool IsExpense { get; }
        internal bool IsIncome { get; }
    }
}