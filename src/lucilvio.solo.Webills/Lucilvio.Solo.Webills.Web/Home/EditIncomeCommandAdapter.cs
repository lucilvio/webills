using System;
using System.Globalization;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditIncomeCommandAdapter : EditIncomeCommand
    {
        public EditIncomeCommandAdapter(EditIncomeViewModel viewModel)
        {
            if (viewModel == null)
                return;

            base.Name = viewModel.Name;
            base.Number = new Guid(viewModel.Number);
            base.Date = DateTime.ParseExact(viewModel.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            base.Value = new TransactionValue(viewModel.Value.MoneyToDecimal());
        }
    }
}