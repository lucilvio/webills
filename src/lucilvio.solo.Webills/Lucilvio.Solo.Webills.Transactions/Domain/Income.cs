using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class Income
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
        public string Name { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }

        class Error
        {
            internal class IncomeMustHaveName : Exception { }
            internal class IncomeTransactionValueCannotBeNull : Exception { }
        }
    }
}