using System;

namespace Lucilvio.Solo.Webills.Core.Domain.User
{
    public class DateRange
    {
        public DateRange(DateTime from, DateTime until)
        {
            this.From = from;
            this.Until = until;
        }

        public DateTime From { get; private set; }
        public DateTime Until { get; private set; }
    }
}