using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomesByFilter
{
    public record FoundIncomesByFilter
    {
        public FoundIncomesByFilter(IEnumerable<FilteredIncome> incomes)
        {
            if (incomes != null)
                this.Incomes = incomes;
        }

        public IEnumerable<FilteredIncome> Incomes { get; } = new List<FilteredIncome>();

        public class FilteredIncome
        {
            internal FilteredIncome() { }

            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public DateTime Date { get; internal set; }
            public decimal Value { get; internal set; }
        }
    }
}