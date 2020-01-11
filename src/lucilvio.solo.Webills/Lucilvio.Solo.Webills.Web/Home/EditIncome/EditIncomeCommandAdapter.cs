using System;
using Lucilvio.Solo.Webills.Web.Shared;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.Core.Domain.User;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditIncomeCommandAdapter : EditIncomeCommand
    {
        public EditIncomeCommandAdapter(EditIncomeRequest viewModel)
        {
            if (viewModel == null)
                return;

            base.Name = viewModel.Name;
            base.Id = new Guid(viewModel.Id);
            base.Date = viewModel.Date.StringToDate();
            base.Value = new TransactionValue(viewModel.Value.MoneyToDecimal());
        }
    }
}