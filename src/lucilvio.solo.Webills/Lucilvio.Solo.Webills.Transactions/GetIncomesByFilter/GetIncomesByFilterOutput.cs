using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.FinancialControl.GetIncomesByFilter
{
    public class GetIncomesByFilterOutput
    {
        public GetIncomesByFilterOutput(IEnumerable<Income> incomes)
        {
            if (incomes != null)
                this.Incomes = incomes;
        }

        public IEnumerable<Income> Incomes { get; } = new List<Income>();

        public class Income
        {
            internal Income() { }

            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public DateTime Date { get; internal set; }
            public decimal Value { get; internal set; }
        }
    }
}