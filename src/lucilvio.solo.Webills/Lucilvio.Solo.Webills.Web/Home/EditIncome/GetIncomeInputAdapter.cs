using System;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions.GetIncome;

namespace Lucilvio.Solo.Webills.Clients.Web.Home.EditIncome
{
    public class GetIncomeInputAdapter : IGetIncomeInput
    {
        private readonly AuthenticatedUser _user;
        private readonly GetIncomeRequest _request;

        public GetIncomeInputAdapter(AuthenticatedUser user, GetIncomeRequest request)
        {
            _user = user;
            _request = request;
        }

        public Guid Id => _request.Id;
        public Guid UserId => this._user.Id;
    }
}