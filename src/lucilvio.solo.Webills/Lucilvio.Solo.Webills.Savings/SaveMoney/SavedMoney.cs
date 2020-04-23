using System;

using Lucilvio.Solo.Webills.Savings.Domain;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    public class SavedMoney
    {
        internal SavedMoney(Guid userId, TransactionValue value)
        {
            this.Value = value.Value;
            this.UserId = userId;
        }

        public decimal Value { get; }
        public Guid UserId { get; }
    }
}