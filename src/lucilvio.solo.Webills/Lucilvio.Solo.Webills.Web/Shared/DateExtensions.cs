using System;
using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Shared
{
    public static class DateExtensions
    {
        public static DateTime StringToDate(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
                return DateTime.MinValue;

            return DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
