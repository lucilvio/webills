using System;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    class DateRange
    {
        public DateRange(DateTime from, DateTime until)
        {
            From = from;
            Until = until;
        }

        public DateTime From { get; private set; }
        public DateTime Until { get; private set; }
    }
}