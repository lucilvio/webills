using System;
using System.Globalization;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;

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
            this.Value = !string.IsNullOrEmpty(viewModel.Value) ? new TransactionValue(decimal.Parse(viewModel.Value, CultureInfo.InvariantCulture)) : TransactionValue.Zero;
        }
    }
}