using System;

namespace Lucilvio.Solo.Webills.Tests
{
    public class Income
    {
        private string v1;
        private DateTime dateTime;

        public Income(string v1, DateTime dateTime, TransactionValue value)
        {
            this.v1 = v1;
            this.dateTime = dateTime;

            if (value == null)
                throw new IncomeTransactionValueCannotBeNull();

            this.Value = value;
        }

        public TransactionValue Value { get; }
    }
}