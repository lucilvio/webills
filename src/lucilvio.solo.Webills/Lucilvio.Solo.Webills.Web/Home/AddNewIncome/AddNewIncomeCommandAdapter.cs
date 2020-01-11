using Lucilvio.Solo.Webills.Core.Domain.User;
using Lucilvio.Solo.Webills.Core.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddNewIncomeCommandAdapter : AddNewIncomeCommand
    {
        public AddNewIncomeCommandAdapter(AddNewIncomeRequest request)
        {
            if (request == null)
                return;

            this.Name = request.Name;
            this.Date = request.Date.StringToDate();
            this.Value = new TransactionValue(request.Value.MoneyToDecimal());
        }
    }
}