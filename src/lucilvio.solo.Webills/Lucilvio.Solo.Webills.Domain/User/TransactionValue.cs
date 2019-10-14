using Lucilvio.Solo.Webills.Domain.User.BusinessErrors;

namespace Lucilvio.Solo.Webills.Domain.User
{
    public class TransactionValue
    {
        private readonly decimal _value;

        public TransactionValue(decimal value)
        {
            if (value < 0)
                throw new TransactionValueCannotBeNegative();

            this._value = value;
        }

        public static TransactionValue Zero => new TransactionValue(0);

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
    }
}