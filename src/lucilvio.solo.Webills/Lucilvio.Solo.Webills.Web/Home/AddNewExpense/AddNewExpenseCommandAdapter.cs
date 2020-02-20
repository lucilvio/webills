using System;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseCommandAdapter : AddNewExpenseCommand
    {
        public AddNewExpenseCommandAdapter(AddNewExpenseRequest request)
        {
            if (request == null)
                return;

            this.UserId = new Guid(request.UserId);
            this.Name = request.Name;
            this.Date = request.Date.StringToDate();
            this.Value = request.Value.MoneyToDecimal();
            this.Category = request.Category;
        }
    }
}