using System;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home.EditIncome
{
    public class EditIncomeResponse
    {
        public EditIncomeResponse(GetUserIncomeQueryResult result)
        {
            if (result == null || result.Number == Guid.Empty)

                return;

            this.Name = result.Name;
            this.Number = result.Number.ToString();
            this.Date = result.Date.ToDateString();
            this.Value = result.Value.Value.DecimalToMoney();
        }

        public string Number { get; private set; }
        public string Name { get; private set; }
        public string Date { get; private set; }
        public string Value { get; private set; }
    }
}
