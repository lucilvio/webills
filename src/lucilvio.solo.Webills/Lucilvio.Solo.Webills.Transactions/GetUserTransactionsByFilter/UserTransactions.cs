using System;
using System.Linq;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.GetUserTransactionsByFilter
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

        public class Transaction
        {
            public Transaction(Guid id, string name, DateTime date, Guid userId, decimal value, string type)
            {
                this.Id = id;
                this.Name = name;
                this.Date = date;
                this.UserId = userId;
                this.Value = value;
                this.Type = type;
            }

            public Guid Id { get; }
            public string Name { get; }
            public DateTime Date { get; }
            public Guid UserId { get; }
            public decimal Value { get; }
            public string Type { get; }
        }
    }
}