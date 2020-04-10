using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Dashboard;
using Lucilvio.Solo.Webills.Transactions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly DashboardModule _dashboardModule;

        public HomeController(IAuthentication authentication, DashboardModule dashboardModule, TransactionsModule transactionsModule)
        {
            this._authentication = authentication;
            this._dashboardModule = dashboardModule;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var loggedUser = this._authentication.User();
            //var result = await this._dashboardModule.ExecuteQuery<GetUserDashboardQueryResult>(new GetUserDashboardQuery(loggedUser.Id));

            return this.View(new UserTransactionsInformationResponse(null));
        }
    }
}