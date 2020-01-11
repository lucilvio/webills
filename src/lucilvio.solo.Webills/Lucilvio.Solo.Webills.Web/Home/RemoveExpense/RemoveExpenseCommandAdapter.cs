using System;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveExpenseCommandAdapter : RemoveExpenseCommand
    {
        public RemoveExpenseCommandAdapter(RemoveExpenseRequest request)
            : base(new Guid(request.UserId), new Guid(request.ExpenseId))
        {
        }
    }
}