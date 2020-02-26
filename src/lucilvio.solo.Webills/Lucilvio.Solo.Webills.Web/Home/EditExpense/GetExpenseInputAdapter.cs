using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.GetExpense;

namespace Lucilvio.Solo.Webills.Clients.Web.Home.EditExpense
{
    public class GetExpenseInputAdapter : IGetExpenseInput
    {
        private readonly AuthenticatedUser _user;
        private readonly GetExpenseRequest _request;

        public GetExpenseInputAdapter(AuthenticatedUser user, GetExpenseRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;
        public Guid Id => this._request.Id;
    }
}
