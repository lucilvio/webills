using System;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveExpenseCommandAdapter : RemoveExpenseCommand
    {
        public RemoveExpenseCommandAdapter(RemoveExpenseViewModel viewModel)
        {
            base.ExpenseNumber = new Guid(viewModel.ExpenseNumber);
        }
    }
}