using System;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveExpenseCommandAdapter : RemoveExpenseCommand
    {
        public RemoveExpenseCommandAdapter(RemoveExpenseRequest request)
        {
            base.ExpenseNumber = new Guid(request.ExpenseNumber);
        }
    }
}