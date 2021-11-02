using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    internal class Recurrency
    {
        public Recurrency(Frequency frequency, DateTime until)
        {
            this.Until = until;
            this.Frequency = frequency;
        }

        public DateTime Until { get; }
        public Frequency Frequency { get; }

        public IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from)
            => this.Frequency.DatesUntilRecurrencyEndsByFrequency(from, this.Until);
    }
}