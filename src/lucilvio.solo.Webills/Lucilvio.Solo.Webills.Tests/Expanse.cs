using System;

namespace Lucilvio.Solo.Webills.Tests
{
    public class Expanse
    {
        private string v1;
        private DateTime dateTime;

        public Expanse(string v1, DateTime dateTime, TransactionValue value)
        {
            this.v1 = v1;
            this.dateTime = dateTime;

            if (value == null)
                throw new ExpanseTransactionValueCannotBeNull();

            this.Value = value;
        }

        public TransactionValue Value { get; }
    }
}