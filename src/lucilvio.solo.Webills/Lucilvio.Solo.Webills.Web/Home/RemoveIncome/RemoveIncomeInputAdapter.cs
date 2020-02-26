using System;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    internal class RemoveIncomeInputAdapter : IRemoveIncomeInput
    {
        private readonly AuthenticatedUser _user;
        private readonly RemoveIncomeRequest _request;

        public RemoveIncomeInputAdapter(AuthenticatedUser user, RemoveIncomeRequest request)
        {
            this._user = user;
            this._request = request;
        }

        public Guid UserId => this._user.Id;

        public Guid IncomeId => this._request.IncomeId;
    }
}