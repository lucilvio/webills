using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.GetUserDashboardInfo;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthenticationService _authentication;
        private readonly TransactionsModule _transactionsModule;

        public HomeController(IAuthenticationService authentication, TransactionsModule transactionsModule)
        {
            this._authentication = authentication;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var loggedUser = this._authentication.User();
                
                var result = await this._transactionsModule.SendMessage<GetDashboardInfoByFilterInput, GetDashboardInfoByFilterOutput>
                    (new GetDashboardInfoByFilterInput(loggedUser.Id));

                return this.View(new UserTransactionsInformationResponse(result));
            }
            catch (System.Exception)
            {
                return this.View(new UserTransactionsInformationResponse(GetDashboardInfoByFilterOutput.Empty));
            }
        }
    }
}