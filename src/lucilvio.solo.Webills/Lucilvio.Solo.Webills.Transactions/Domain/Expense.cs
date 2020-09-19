using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class Expense
    {
        private Expense()
        {
            Id = Guid.NewGuid();
        }

        private Expense(string name, Category category, DateTime date, TransactionValue value) : this()
        {
            this.Validate(name, date, value);

            Name = name;
            Date = date;
            Category = category;
            Value = value;
        }

        private Expense(string name, Category category, DateTime date, TransactionValue value, RecurrentExpense recurrentExpenses) 
            : this(name, category, date, value)
        {
            this.RecurrentExpenses = recurrentExpenses;
        }

        public static Expense New(string name, Category category, DateTime date, TransactionValue value)
            => new Expense(name, category, date, value);

        public static Expense NewWithRecurrency(string name, Category category, DateTime date, TransactionValue value,
            RecurrentExpense recurrency) => new Expense(name, category, date, value, recurrency);

        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public Category Category { get; private set; }
        public TransactionValue Value { get; private set; }
        public RecurrentExpense RecurrentExpenses { get; }

        internal void Change(string name, Category category, DateTime date, TransactionValue value)
        {
            this.Validate(name, date, value);

            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
        }
        
        private void Validate(string name, DateTime date, TransactionValue value)
        {
            if (string.IsNullOrEmpty(name))
                throw new Error.ExpenseMustHaveName();

            if (date < DateTime.Now.AddYears(-100))
                throw new Error.ExpenseCannotBeOlderThanOneHundredYears();

            if (value == null)
                throw new Error.ExpenseTransactionValueCannotBeNull();
        }

        class Error
        {
            internal class ExpenseMustHaveName : Exception { }
            internal class ExpenseCannotBeOlderThanOneHundredYears : Exception { }
            internal class ExpenseTransactionValueCannotBeNull : Exception { }
        }

    }
}