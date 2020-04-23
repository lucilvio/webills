using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Savings.Domain
{
    internal class SavingsAccount
    {
        private readonly IList<Transaction> _transactions;

        internal SavingsAccount()
        {
            this._transactions = new List<Transaction>();
        }

        public Guid Id { get; }

        internal void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new Error.CannotInsertEmptyTransactionInSavingsAccount();

            this._transactions.Add(transaction);
        }

        class Error
        {
            public class CannotInsertEmptyTransactionInSavingsAccount : Exception { }
        }
    }
}