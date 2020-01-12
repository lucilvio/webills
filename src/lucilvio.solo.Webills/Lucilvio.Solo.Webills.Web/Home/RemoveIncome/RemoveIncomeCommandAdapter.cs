using System;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.RemoveIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveIncomeCommandAdapter : RemoveIncomeCommand
    {
        public RemoveIncomeCommandAdapter(RemoveIncomeRequest request)
        {
            base.IncomeNumber = new Guid(request.IncomeId);
        }
    }
}