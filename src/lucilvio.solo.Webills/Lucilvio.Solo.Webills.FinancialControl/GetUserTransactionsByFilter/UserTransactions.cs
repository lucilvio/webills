using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public class UserTransactions
    {
        private readonly IList<Transaction> _transactions;

        internal UserTransactions()
        {
            this._transactions = new List<Transaction>();
        }

        internal UserTransactions(IEnumerable<Transaction> transactions) : this()
        {
            if (transactions is null)
                return;

            this._transactions = transactions.ToList();
        }

        public IEnumerable<Transaction> Transactions => this._transactions;

        public record Transaction(Guid Id, Guid? RecurrencyId, string Name, DateTime Date, Guid UserId, decimal Value, string Type);
    }
}