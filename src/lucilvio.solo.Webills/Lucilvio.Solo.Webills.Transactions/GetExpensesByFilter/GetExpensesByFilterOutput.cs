using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter
{
    public class GetExpensesByFilterOutput
    {
        internal GetExpensesByFilterOutput(IEnumerable<Expense> expenses)
        {
            if(expenses != null)
                this.Expenses = expenses;
        }

        public IEnumerable<Expense> Expenses { get; } = new List<Expense>();

        public class Expense
        {
            internal Expense() { }

            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public DateTime Date { get; internal set; }
            public string Category { get; internal set; }
            public decimal Value { get; internal set; }
        }
    }
}