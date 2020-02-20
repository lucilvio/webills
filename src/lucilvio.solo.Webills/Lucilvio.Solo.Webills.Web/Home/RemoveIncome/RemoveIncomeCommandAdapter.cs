using System;

using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveIncomeCommandAdapter : RemoveIncomeCommand
    {
        public RemoveIncomeCommandAdapter(RemoveIncomeRequest request)
        {
            base.IncomeId = new Guid(request.IncomeId);
        }
    }
}