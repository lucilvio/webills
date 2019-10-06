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
            this.Date = expense.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Value = expense.Value.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Name { get; }
        public string Date { get; }
        public string Value { get; }
    }
}