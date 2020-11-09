using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal class Income
    {
        private Income()
        {
            Id = Guid.NewGuid();
        }

        internal Income(string name, DateTime date, IncomeCategory category, TransactionValue value) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new Error.IncomeMustHaveName();

            this.Name = name;
            this.Date = date;

            if (value == null)
                throw new Error.IncomeTransactionValueCannotBeNull();

            this.Value = value;

            this.Category = category;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public IncomeCategory Category { get; set; }
        public TransactionValue Value { get; private set; }

        internal void Change(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        public enum IncomeCategory
        {
            Salary = 1,
            Investments = 2,
            Other = 3
        }

        class Error
        {
            internal class IncomeMustHaveName : Exception { }
            internal class IncomeTransactionValueCannotBeNull : Exception { }
        }
    }
}