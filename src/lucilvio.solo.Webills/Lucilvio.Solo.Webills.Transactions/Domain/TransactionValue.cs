using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    internal record TransactionValue
    {
        private readonly decimal _value;

        public TransactionValue(decimal value)
        {
            if (value < 0)
                throw new Error.TransactionValueCannotBeNegative();

            _value = value;
        }

        public decimal Value => _value;

        class Error
        {
            internal class TransactionValueCannotBeNegative : Exception { }
        }
    }
}