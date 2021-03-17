using System;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal class Expense
    {
        private Expense()
        {
            this.Id = Guid.NewGuid();
        }

        public Expense(Guid userId, string name, string category, DateTime date, TransactionValue value) : this()
        {
            this.Validate(userId, name, date, value);
            
            this.Date = date;
            this.Name = name;
            this.Value = value;
            this.UserId = userId;

            if (!Enum.TryParse(typeof(ExpenseCategory), category, out var categoryEnum))
                this.Category = ExpenseCategory.Others;

            this.Category = (ExpenseCategory)categoryEnum;
        }

        internal Expense(Guid userId, string name, string category, DateTime date, TransactionValue value, Guid recurrentExpenseId)
            : this(userId, name, category, date, value)
        {
            this.RecurrentExpenseId = recurrentExpenseId;
        }

        public Guid Id { get; }
        public Guid UserId { get; set; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public ExpenseCategory Category { get; private set; }
        public TransactionValue Value { get; private set; }
        public Guid? RecurrentExpenseId { get; }

        internal void Update(Guid userId, string name, ExpenseCategory category, DateTime date, TransactionValue value)
        {
            this.Validate(userId, name, date, value);

            this.Name = name;
            this.Category = category;
            this.Date = date;
            this.Value = value;
        }

        private void Validate(Guid userId, string name, DateTime date, TransactionValue value)
        {
            if (userId == Guid.Empty)
                throw new Error.ExpenseMustHaveUserId();

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
            internal class ExpenseMustHaveUserId : Exception { }
            internal class ExpenseMustHaveName : Exception { }
            internal class ExpenseCannotBeOlderThanOneHundredYears : Exception { }
            internal class ExpenseTransactionValueCannotBeNull : Exception { }
        }
    }
}