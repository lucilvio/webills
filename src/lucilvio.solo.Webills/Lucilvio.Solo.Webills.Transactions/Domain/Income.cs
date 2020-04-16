using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal class Income
    {
        private Income()
        {
            Id = Guid.NewGuid();
        }

        internal Income(string name, DateTime date, TransactionValue value) : this()
        {
            if (string.IsNullOrEmpty(name))
                throw new Error.IncomeMustHaveName();

            Name = name;
            Date = date;

            if (value == null)
                throw new Error.IncomeTransactionValueCannotBeNull();

            Value = value;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionValue Value { get; private set; }

        internal void Change(string name, DateTime date, TransactionValue value)
        {
            this.Name = name;
            this.Date = date;
            this.Value = value;
        }

        class Error
        {
            internal class IncomeMustHaveName : Exception { }
            internal class IncomeTransactionValueCannotBeNull : Exception { }
        }

    }
}