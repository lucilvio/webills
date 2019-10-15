using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditExpenseCommandAdapter : EditExpenseCommand
    {
        public EditExpenseCommandAdapter(EditExpenseViewModel viewModel)
        {
            if (viewModel == null)
                return;

            base.Name = viewModel.Name;
            this.Number = new Guid(viewModel.Number);
            base.Date = !string.IsNullOrEmpty(viewModel.Date) ? DateTime.Parse(viewModel.Date) : DateTime.MinValue;
            base.Value = new TransactionValue(decimal.Parse(viewModel.Value));
        }
    }
}