using System;
using Lucilvio.Solo.Webills.Savings.Domain;

namespace Lucilvio.Solo.Webills.Savings.SaveMoney
{
    public class SaveMoneyInput
    {
        public SaveMoneyInput(Guid userId, decimal value)
        {
            this.UserId = userId;
            this.Value = new TransactionValue(value);
        }

        internal Guid UserId { get; }
        internal TransactionValue Value { get; }
    }
}