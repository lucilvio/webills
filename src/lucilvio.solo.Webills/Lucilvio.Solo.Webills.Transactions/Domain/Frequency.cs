using System;
using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.FinancialControl.Domain
{
    public abstract class Frequency
    {
        public Frequency(int value)
        {
            this.Value = value;
        }

        public static Frequency Daily => new DailyFrequency();
        public static Frequency Monthly => new MonthlyFrequency();
        public static Frequency Trimonthly => new TrimonthlyFrequency();
        public static Frequency Yearly = new YearlyFrequency();

        public int Value { get; }

        public static Frequency FromValue(int value)
        {
            var types = new Dictionary<int, Frequency>
            {
                { 1, new DailyFrequency() },
                { 30, new MonthlyFrequency() },
                { 90, new TrimonthlyFrequency() },
                { 365, new YearlyFrequency() },
            };

            if (!types.TryGetValue(value, out var frequency))
                new NoFrequency();

            return frequency;
        }

        public abstract IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until);

        private class DailyFrequency : Frequency
        {
            public DailyFrequency() : base(1) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until)
            {
                for (DateTime date = from.AddDays(1); date <= until; date = date.AddDays(1))
                    yield return date;
            }
        }

        private class MonthlyFrequency : Frequency
        {
            public MonthlyFrequency() : base(2) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until)
            {
                var months = Math.Abs(12 * (until.Year - from.Year) + until.Month - from.Month);

                for (int i = 1; i <= months; i++)
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

        private class TrimonthlyFrequency : Frequency
        {
            public TrimonthlyFrequency() : base(3) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until)
            {
                var months = Math.Abs(12 * (until.Year - from.Year) + until.Month - from.Month);

                for (int i = 3; i <= months; i += 3)
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

        private class YearlyFrequency : Frequency
        {
            public YearlyFrequency() : base(4) { }

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until)
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

            public override IEnumerable<DateTime> DatesUntilRecurrencyEndsByFrequency(DateTime from, DateTime until)
            {
                return new List<DateTime>();
            }
        }
    }
}