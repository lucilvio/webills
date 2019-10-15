using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web
{
    public class MoneyViewModel
    {
        public MoneyViewModel(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            var region = new RegionInfo(Thread.CurrentThread.CurrentUICulture.LCID);

            this.Value = $"{region.ISOCurrencySymbol} {decimal.Parse(value).ToString("C")}";
        }

        public string Value { get; }
    }
}
