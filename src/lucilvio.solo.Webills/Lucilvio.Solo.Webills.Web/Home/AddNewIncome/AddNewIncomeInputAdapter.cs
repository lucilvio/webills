using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class AddNewIncomeInputAdapter : IAddNewIncomeInput
    {
        private readonly AuthenticatedUser _user;
        private readonly AddNewIncomeRequest _request;

        public AddNewIncomeInputAdapter(AuthenticatedUser user, AddNewIncomeRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;

        public string Name => this._request.Name;

        public DateTime Date => this._request.Date.StringToDate();

        public decimal Value => this._request.Value.MoneyToDecimal();
    }
}