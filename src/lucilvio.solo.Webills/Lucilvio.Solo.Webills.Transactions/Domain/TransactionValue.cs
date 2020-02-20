using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class TransactionValue
    {
        private readonly decimal _value;

        public TransactionValue(decimal value)
        {
            if (value < 0)
                throw new Error.TransactionValueCannotBeNegative();

            _value = value;
        }

        public static TransactionValue Zero => new TransactionValue(0);

        public decimal Value => _value;

        public override bool Equals(object obj)
        {
            var tv = obj as TransactionValue;

            if (tv == null)
                return false;

            return Value.Equals(tv.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        class Error
        {
            internal class TransactionValueCannotBeNegative : Exception { }
        }
    }
}