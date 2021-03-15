using System;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal record TransactionValue
    {
        private readonly decimal _value;

        public TransactionValue(decimal value)
        {
            if (value < 0)
                throw new Error.TransactionValueCannotBeNegative();

            this._value = value;
        }

        public decimal Value => this._value;

        class Error
        {
            internal class TransactionValueCannotBeNegative : Exception { }
        }
    }
}