using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter
{
    public class GetIncomesByFilterOutput
    {
        private readonly IEnumerable<Income> _incomes;

        public GetIncomesByFilterOutput(IEnumerable<Income> incomes)
        {
            if(incomes != null)
                this._incomes = incomes;
        }

        public IEnumerable<Income> Incomes { get; } = new List<Income>();

        public class Income
        {
            internal Income() { }

            public Guid Id { get; internal set; }
            public string Name { get; internal set; }
            public DateTime Date { get; internal set; }
            public string Category { get; internal set; }
            public decimal Value { get; internal set; }
        }
    }
}