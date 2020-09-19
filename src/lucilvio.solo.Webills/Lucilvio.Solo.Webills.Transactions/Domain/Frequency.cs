using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Transactions.Domain
{
    public abstract class Frequency
    {
        public Frequency(int value)
        {
            this.Value = value;
        }

        public static Frequency Days => new DailyFrequency();
        public static Frequency Months => new MonthlyFrequency();
        public static Frequency Years = new AnnualFrequency();
        
        public int Value { get; }

        public static Frequency FromValue(int value)
        {
            var types = new Dictionary<int, Frequency>
            {
                { 1, new DailyFrequency() },
                { 2, new MonthlyFrequency() },
                { 3, new AnnualFrequency() },
            };

            if(!types.TryGetValue(value, out var frequency))
                new NoFrequency();

            return frequency;
        }

        public abstract IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(int repetitionCount, DateTime from, DateTime until);

        private class DailyFrequency : Frequency
        {
            public DailyFrequency() : base(1) { }
            
            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(int count, DateTime from, DateTime until)
            {
                for (DateTime date = from.AddDays(count); date <= until; date = date.AddDays(count))
                    yield return date;
            }
        }

        private class MonthlyFrequency : Frequency
        {
            public MonthlyFrequency() : base(2) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(int count, DateTime from, DateTime until)
            {
                var months = Math.Abs(12 * (until.Year - from.Year) + until.Month - from.Month);

                for (int i = count; i <= months; i += count)
                {
                    var newDate = from.AddMonths(i);
                    var day = from.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < from.Day)
                        day = newDate.Day;

                    yield return new DateTime(newDate.Year, newDate.Month, day);
                }
            }
        }

        private class AnnualFrequency : Frequency
        {
            public AnnualFrequency() : base(3) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(int count, DateTime from, DateTime until)
            {
                var years = until.Year - from.Year;

                for (int i = 1; i <= years; i++)
                {
                    var newDate = from.AddYears(i);
                    var day = from.Day;

                    if (newDate > until)
                        continue;

                    if (DateTime.DaysInMonth(newDate.Year, newDate.Month) < from.Day)
                        day = newDate.Day;

                    yield return new DateTime(newDate.Year, from.Month, day);
                }
            }
        }

        private class NoFrequency : Frequency
        {
            public NoFrequency() : base(0) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(int repetitionCount, DateTime from, DateTime until)
            {
                return new List<DateTime>();
            }
        }
    }
}