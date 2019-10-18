using System;
using Lucilvio.Solo.Webills.Web.Shared;

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

            this.Name = result.Name;
            this.Number = result.Number.ToString();
            this.Date = result.Date.ToDateString(); 
            this.Value = result.Value.Value.DecimalToMoney();
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
    }
}