using System;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Transactions.Domain;

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
            public int Category { get; internal set; }
            public string CategoryName => ((Category)this.Category).ToString();
            public decimal Value { get; internal set; }
        }
    }
}