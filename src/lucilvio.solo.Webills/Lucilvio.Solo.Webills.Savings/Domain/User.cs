using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.Savings.Domain
{
    internal class User
    {
        private readonly IList<SavingsAccount> _savingsAccounts;

        internal User(Guid id)
        {
            this.Id = id;

            this._savingsAccounts = new List<SavingsAccount>();
        }

        public Guid Id { get; }
        public IEnumerable<SavingsAccount> SavingsAccounts => this._savingsAccounts;

        internal void SaveMoney(TransactionValue value)
        {
            var foundSavingsAccount = this._savingsAccounts.FirstOrDefault();

            if (foundSavingsAccount == null)
            {
                foundSavingsAccount = new SavingsAccount();
                this._savingsAccounts.Add(foundSavingsAccount);
            }

            foundSavingsAccount.AddTransaction(new Transaction(value));
        }
    }
}