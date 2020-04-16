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
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public Category Category { get; private set; }
        public TransactionValue Value { get; private set; }

        internal void Change(string name, Category category, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
        }

        class Error
        {
            internal class ExpenseMustHaveName : Exception { }
            internal class ExpenseCannotBeOlderThanOneHundredYears : Exception { }
            internal class ExpenseTransactionValueCannotBeNull : Exception { }
        }

    }
}