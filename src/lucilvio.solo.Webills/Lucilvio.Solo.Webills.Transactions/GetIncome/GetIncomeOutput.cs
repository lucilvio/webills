using System;

namespace Lucilvio.Solo.Webills.Transactions.GetIncome
{
    public class GetIncomeOutput
    {
        internal GetIncomeOutput() { }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public string Category { get; internal set; }
        public decimal Value { get; internal set; }
    }
}