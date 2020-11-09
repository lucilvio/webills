using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal class Expense
    {
        private Expense()
        {
            Id = Guid.NewGuid();
        }

        private Expense(string name, ExpenseCategory category, DateTime date, TransactionValue value) : this()
        {
            this.Validate(name, date, value);

            Name = name;
            Date = date;
            Category = category;
            Value = value;
        }

        private Expense(string name, ExpenseCategory category, DateTime date, TransactionValue value, RecurrentExpense recurrentExpenses) 
            : this(name, category, date, value)
        {
            this.RecurrentExpenses = recurrentExpenses;
        }

        public static Expense New(string name, ExpenseCategory category, DateTime date, TransactionValue value)
            => new Expense(name, category, date, value);

        public static Expense NewWithRecurrency(string name, ExpenseCategory category, DateTime date, TransactionValue value,
            RecurrentExpense recurrency) => new Expense(name, category, date, value, recurrency);

        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public ExpenseCategory Category { get; private set; }
        public TransactionValue Value { get; private set; }
        public RecurrentExpense RecurrentExpenses { get; }
         
        internal void Change(string name, ExpenseCategory category, DateTime date, TransactionValue value)
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

        public enum ExpenseCategory
        {
            Others = 0,
            Food = 1,
            Education = 2,
            Shopping = 3,
            Clothing = 4,
            BarAndRestaurant = 5,
            Groceries = 6,
            Health = 7,
            Taxes = 8,
            Transportation = 9,
            Home = 10,
            Investments = 11,
            PersonalCare = 12,
            Entertainment = 13
        }

        class Error
        {
            internal class ExpenseMustHaveName : Exception { }
            internal class ExpenseCannotBeOlderThanOneHundredYears : Exception { }
            internal class ExpenseTransactionValueCannotBeNull : Exception { }
        }
    }
}