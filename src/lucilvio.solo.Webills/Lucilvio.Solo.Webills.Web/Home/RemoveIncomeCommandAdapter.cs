using System;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveIncomeCommandAdapter : RemoveIncomeCommand
    {
        public RemoveIncomeCommandAdapter(RemoveIncomeRequest request)
        {
            base.IncomeNumber = new Guid(request.IncomeNumber);
        }
    }
}