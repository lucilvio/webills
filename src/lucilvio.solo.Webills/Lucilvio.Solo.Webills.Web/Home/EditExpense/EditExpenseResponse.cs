using Lucilvio.Solo.Webills.Web.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web.Home.EditExpense
{
    public class EditExpenseResponse
    {
        public EditExpenseResponse(GetUserExpensesQueryResult result)
        {
            if (result == null)
                return;

            this.Name = result.Name;
            this.Number = result.Number.ToString();
            this.Date = result.Date.ToDateString();
            this.Category = result.Category.ToString();
            this.Value = result.Value.Value.DecimalToMoney();
        }

        public string Number { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Category { get; set; }
        public string Value { get; set; }
    }
}
