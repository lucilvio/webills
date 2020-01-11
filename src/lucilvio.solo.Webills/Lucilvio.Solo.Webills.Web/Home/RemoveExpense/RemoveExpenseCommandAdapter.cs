using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveExpense;
using System;

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