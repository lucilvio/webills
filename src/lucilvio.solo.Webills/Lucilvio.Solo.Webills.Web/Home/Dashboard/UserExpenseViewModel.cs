using Lucilvio.Solo.Webills.Web.Shared;
using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Home.Index
{
    public class UserExpenseViewModel
    {
        public UserExpenseViewModel(UserExpenseData expense)
        {
            if (expense == null)
                return;

            this.Name = expense.Name;
            this.Number = expense.Number.ToString();
            this.Category = expense.Category.ToString();
            this.Date = expense.Date.ToDateString();
            this.Value = expense.Value.Value.DecimalToMoney();
        }

        public string Number { get;  }
        public string Category { get;  }
        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}