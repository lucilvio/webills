using System;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditExpenseCommandAdapter : EditExpenseCommand
    {
        public EditExpenseCommandAdapter(EditExpenseRequest request)
        {
            if (request == null)
                return;

            base.Name = request.Name;
            this.Id = new Guid(request.Id);
            base.Date = request.Date.StringToDate();
            base.Value = request.Value.MoneyToDecimal();
            base.Category = request.Category;
        }
    }
}