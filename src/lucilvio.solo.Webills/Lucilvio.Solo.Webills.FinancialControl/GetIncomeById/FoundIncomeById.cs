using System;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomeById
{
    public class FoundIncomeById
    {
        internal FoundIncomeById() { }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public decimal Value { get; internal set; }
    }
}