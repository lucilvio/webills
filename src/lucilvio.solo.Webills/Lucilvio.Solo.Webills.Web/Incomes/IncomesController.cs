using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IAuthentication _auth;

        public IncomesController(IAuthentication auth)
        {
            this._auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromServices]TransactionsModule transactionsModule)
        {
            var result = await transactionsModule.GetIncomesByFilter(new GetIncomesByFilterInput(this._auth.User().Id));

            return this.View(new IncomesResponse(result));
        }
    }
}
