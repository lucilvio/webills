using System;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpenseById
{
    public class FoundExpenseById
    {
        internal FoundExpenseById() { }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public int Category { get; internal set; }
        public decimal Value { get; internal set; }
    }
}