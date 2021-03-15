using System;

namespace Lucilvio.Solo.Webills.FinancialControl.GetExpense
{
    public class GetExpenseByIdOutput
    {
        internal GetExpenseByIdOutput() { }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }
        public DateTime Date { get; internal set; }
        public int Category { get; internal set; }
        public decimal Value { get; internal set; }
    }
}