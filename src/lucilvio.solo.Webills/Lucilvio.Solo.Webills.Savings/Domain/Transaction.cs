using System;

namespace Lucilvio.Solo.Webills.Savings.Domain
{
    internal class Transaction
    {
        internal Transaction(TransactionValue value)
        {
            this.Value = value;
            this.Date = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime Date { get; }
        public TransactionValue Value { get; }
    }
}
