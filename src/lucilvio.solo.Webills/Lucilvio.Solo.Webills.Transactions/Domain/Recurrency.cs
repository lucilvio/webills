using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    public class Recurrency
    {
        public Recurrency(int repetitionCount, Frequency frequency, DateTime until)
        {
            this.Until = until;
            this.Frequency = frequency;
            this.RepetitionCount = repetitionCount;
        }

        public DateTime Until { get; }
        public Frequency Frequency { get; }
        public int RepetitionCount { get; }

        public IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from)
            => this.Frequency.DatesUntilRecurrencyEndsByFrequency(this.RepetitionCount, from, this.Until);
    }
}