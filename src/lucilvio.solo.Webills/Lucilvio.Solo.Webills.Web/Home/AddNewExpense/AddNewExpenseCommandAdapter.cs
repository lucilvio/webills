using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using System;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseCommandAdapter : AddNewExpenseCommand
    {
        public AddNewExpenseCommandAdapter(AddNewExpenseViewModel viewModel)
        {
            if (viewModel == null)
                return;

            this.Name = viewModel.Name;
            this.Date = viewModel.Date.StringToDate();
            this.Value = new TransactionValue(viewModel.Value.MoneyToDecimal());
            this.Category = (Category)Enum.Parse(typeof(Category), viewModel.Category);
        }
    }
}