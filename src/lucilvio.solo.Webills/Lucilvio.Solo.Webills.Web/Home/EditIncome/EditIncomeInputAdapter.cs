using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditIncomeInputAdapter : IEditIncomeInput
    {
        private readonly AuthenticatedUser _user;
        private readonly EditIncomeRequest _request;

        public EditIncomeInputAdapter(AuthenticatedUser user, EditIncomeRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;

        public Guid Id => this._request.Id;

        public string Name => this._request.Name;

        public DateTime Date => this._request.Date.StringToDate();

        public decimal Value => this._request.Value.MoneyToDecimal();
    }
}