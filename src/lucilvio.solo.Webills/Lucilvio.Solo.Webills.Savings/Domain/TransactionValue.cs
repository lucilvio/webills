using System;

namespace Lucilvio.Solo.Webills.Savings.Domain
{
    internal class TransactionValue
    {
        private readonly decimal _value;

        public TransactionValue(decimal value)
        {
            if (value < 0)
                throw new Error.TransactionValueCannotBeNegative();

            this._value = value;
        }

        public decimal Value => this._value;

        public override bool Equals(object obj)
        {
            var tv = obj as TransactionValue;

            if (tv == null)
                return false;

            return this.Value.Equals(tv.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        class Error
        {
            internal class TransactionValueCannotBeNegative : Exception { }
        }
    }
}
