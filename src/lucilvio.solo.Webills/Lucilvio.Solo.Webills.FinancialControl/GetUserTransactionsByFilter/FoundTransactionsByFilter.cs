using System;
using System.Collections.Generic;
using System.Linq;

namespace Lucilvio.Solo.Webills.FinancialControl.GetUserTransactionsByFilter
{
    public record FoundTransactionsByFilter
    {
        private readonly IList<FilteredTransaction> _transactions;

        internal FoundTransactionsByFilter()
        {
            this._transactions = new List<FilteredTransaction>();
        }

        internal FoundTransactionsByFilter(IEnumerable<FilteredTransaction> transactions) : this()
        {
            if (transactions is null)
                return;

            this._transactions = transactions.ToList();
        }

        public IEnumerable<FilteredTransaction> Transactions => this._transactions;

        public record FilteredTransaction(Guid Id, Guid? RecurrencyId, string Name, DateTime Date, Guid UserId, decimal Value, string Type);
    }
}