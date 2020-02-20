using System;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditIncomeCommandAdapter : EditIncomeCommand
    {
        public EditIncomeCommandAdapter(EditIncomeRequest request)
        {
            if (request == null)
                return;

            base.Name = request.Name;
            base.Id = new Guid(request.Id);
            base.Date = request.Date.StringToDate();
            base.Value = request.Value.MoneyToDecimal();
        }
    }
}