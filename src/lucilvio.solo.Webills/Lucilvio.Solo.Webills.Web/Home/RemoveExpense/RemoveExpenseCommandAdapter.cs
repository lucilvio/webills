using System;

using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveExpenseCommandAdapter : RemoveExpenseCommand
    {
        public RemoveExpenseCommandAdapter(RemoveExpenseRequest request)
        {
            base.UserId = Guid.Parse(request.UserId);
            base.ExpenseId = Guid.Parse(request.ExpenseId);
        }
    }
}