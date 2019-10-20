using System;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveIncomeCommandAdapter : RemoveIncomeCommand
    {
        public RemoveIncomeCommandAdapter(RemoveIncomeViewModel viewModel)
        {
            base.IncomeNumber = new Guid(viewModel.IncomeNumber);
        }
    }
}