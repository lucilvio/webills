using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class AddNewExpenseInputAdapter : IAddNewExpenseInput
    {
        private readonly AuthenticatedUser _user;
        private readonly AddNewExpenseRequest _request;

        public AddNewExpenseInputAdapter(AuthenticatedUser user, AddNewExpenseRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;

        public string Name => this._request.Name;

        public string Category => this._request.Category;

        public DateTime Date => this._request.Date.StringToDate();

        public decimal Value => this._request.Value.MoneyToDecimal();
    }
}