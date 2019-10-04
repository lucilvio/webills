using System;
using Lucilvio.Solo.Webills.Tests;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseCommandAdapter : AddNewExpenseCommand
    {
        public AddNewExpenseCommandAdapter(AddNewExpenseViewModel viewModel)
        {
            if (viewModel == null)
                return;

            this.Name = viewModel.Name;
            this.Date = !string.IsNullOrEmpty(viewModel.Date) ? DateTime.Parse(viewModel.Date) : DateTime.MinValue;
            this.Value = !string.IsNullOrEmpty(viewModel.Value) ? new TransactionValue(decimal.Parse(viewModel.Value)) : TransactionValue.Zero;
        }
    }
}