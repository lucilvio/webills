using System;
using System.Threading;
using System.Globalization;

namespace Lucilvio.Solo.Webills.Web
{
    public static class MoneyExtensions
    {
        public static string DecimalToMoney(this decimal value)
        {
            return $"{value.ToString("C2", GetNumberFormarter())}";
        }

        public static decimal MoneyToDecimal(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return 0m;

            if (char.IsNumber(value[0]))
                value = $"{new RegionInfo(GetThreadCulture().LCID).CurrencySymbol} {value}";
            
            return decimal.Parse(value, NumberStyles.Number | NumberStyles.Currency, GetNumberFormarter());
        }

        private static IFormatProvider GetNumberFormarter()
        {
            var numberFormater = new CultureInfo("en-US").NumberFormat;
            
            numberFormater.CurrencyDecimalDigits = 2;
            numberFormater.CurrencyGroupSeparator = ".";
            numberFormater.CurrencyDecimalSeparator = ",";
            numberFormater.CurrencyNegativePattern = 1; 
            numberFormater.CurrencySymbol = $"{new RegionInfo(GetThreadCulture().LCID).CurrencySymbol} ";

            return numberFormater;
        }

        private static CultureInfo GetThreadCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }
    }
}
