using System;

namespace Lucilvio.Solo.Webills.Transactions.GetExpense
{
    public class GetExpenseOutput
    {
        internal GetExpenseOutput() { }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public string Category { get; internal set; }
        public decimal Value { get; internal set; }
    }
}