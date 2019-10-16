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
            this.Date = expense.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Value = expense.Value.Value.ToMoney();
        }

        public string Number { get; set; }
        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}