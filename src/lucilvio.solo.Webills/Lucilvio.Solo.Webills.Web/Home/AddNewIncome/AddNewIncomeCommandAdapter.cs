using System;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddNewIncomeCommandAdapter : AddNewIncomeCommand
    {
        public AddNewIncomeCommandAdapter(AddNewIncomeViewModel viewModel)
        {
            if (viewModel == null)
                return;

            this.Name = viewModel.Name;
            this.Date = viewModel.Date.StringToDate();
            this.Value = new TransactionValue(viewModel.Value.MoneyToDecimal());
        }
    }
}