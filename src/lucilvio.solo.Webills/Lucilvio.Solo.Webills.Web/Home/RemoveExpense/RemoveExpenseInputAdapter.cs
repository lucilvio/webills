using System;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveExpenseInputAdapter : IRemoveExpenseInput
    {
        private readonly AuthenticatedUser _user;
        private readonly RemoveExpenseRequest _request;

        public RemoveExpenseInputAdapter(AuthenticatedUser user, RemoveExpenseRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;
        public Guid ExpenseId => this._request.ExpenseId;
    }
}