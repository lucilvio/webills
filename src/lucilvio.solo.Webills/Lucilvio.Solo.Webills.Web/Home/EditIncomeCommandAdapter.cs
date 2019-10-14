using System;
using System.Globalization;
using Lucilvio.Solo.Webills.Domain.User;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditIncomeCommandAdapter : EditIncomeCommand
    {
        private EditIncomeViewModel viewModel;

        public EditIncomeCommandAdapter(EditIncomeViewModel viewModel)
        {
            if (viewModel == null)
                return;

            base.Name = viewModel.Name;
            base.Number = new Guid(viewModel.Number);
            base.Date = DateTime.Parse(viewModel.Date, CultureInfo.InvariantCulture);
            base.Value = new TransactionValue(decimal.Parse(viewModel.Value));
        }
    }
}