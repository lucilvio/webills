using System.Globalization;
using System.Threading;

namespace Lucilvio.Solo.Webills.Web
{
    public static class DecimalExtensions
    {
        public static string ToMoney(this decimal value)
        {
            var region = new RegionInfo(Thread.CurrentThread.CurrentUICulture.LCID);
            return $"{value.ToString("N2", new NumberFormatInfo { NumberDecimalSeparator = "," })} {region.CurrencySymbol}";
        }
    }
}
