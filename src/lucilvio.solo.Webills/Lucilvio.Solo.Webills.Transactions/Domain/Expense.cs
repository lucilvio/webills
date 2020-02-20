using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class Expense
    {
        private Expense()
        {
            Id = Guid.NewGuid();
        }

        internal Expense(string name, Category category, DateTime date, TransactionValue value) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new Error.ExpenseMustHaveName();

            if (date < DateTime.Now.AddYears(-100))
                throw new Error.ExpenseCannotBeOlderThanOneHundredYears();

            if (value == null)
                throw new Error.ExpenseTransactionValueCannotBeNull();

            Name = name;
            Date = date;
            Category = category;
            Value = value;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Date { get; }
        public Category Category { get; }
        public TransactionValue Value { get; }

        class Error
        {
            internal class ExpenseMustHaveName : Exception { }
            internal class ExpenseCannotBeOlderThanOneHundredYears : Exception { }
            internal class ExpenseTransactionValueCannotBeNull : Exception { }
        }
    }
}