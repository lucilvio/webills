using System;
using Lucilvio.Solo.Webills.Web.Shared;
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
            base.Date = viewModel.Date.StringToDate();
            base.Value = new TransactionValue(viewModel.Value.MoneyToDecimal());
            base.Category = (Category)Enum.Parse(typeof(Category), viewModel.Category);
        }
    }
}