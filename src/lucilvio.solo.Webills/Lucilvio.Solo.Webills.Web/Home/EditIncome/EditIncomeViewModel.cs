using System;
using System.Globalization;

namespace Lucilvio.Solo.Webills.Web.Home.EditIncome
{
    public class EditIncomeViewModel
    {
        public EditIncomeViewModel()
        {
        }

        public EditIncomeViewModel(SearchForUserIncomeByNumberResult result)
        {
            if (result == null || result.Number == Guid.Empty)

                return;

            this.Number = result.Number.ToString();
            this.Name = result.Name;
            this.Date = result.Date.ToString("dd/MM/yyyy");
            this.Value = result.Value.Value.ToMoney();
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}
