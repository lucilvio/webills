using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Web.Shared;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class EditExpenseCommandAdapter : IEditExpenseInput
    {
        private readonly AuthenticatedUser _user;
        private readonly EditExpenseRequest _request;

        public EditExpenseCommandAdapter(AuthenticatedUser user, EditExpenseRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;

        public Guid Id => this._request.Id;

        public string Name => this._request.Name;

        public string Category => this._request.Category;

        public DateTime Date => this._request.Date.StringToDate();

        public decimal Value => this._request.Value.MoneyToDecimal();
    }
}