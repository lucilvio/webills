using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserIncomeViewModel
    {
        public UserIncomeViewModel(UserIncomeData income)
        {
            if (income == null)
                return;

            this.Name = income.Name;
            this.Date = income.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Value = income.Value.Value.ToString(CultureInfo.InvariantCulture);
        }

        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }

}