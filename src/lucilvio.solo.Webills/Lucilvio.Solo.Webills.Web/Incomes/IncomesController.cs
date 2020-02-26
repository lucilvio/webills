using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    [Authorize]
    public class IncomesController : Controller
    {
        private readonly IAuthentication _auth;
        private readonly IGetUserIncomesByFilterQueryHandler _incomesQueryHandler;

        public IncomesController(IAuthentication auth, IGetUserIncomesByFilterQueryHandler incomesQueryHandler)
        {
            this._auth = auth;
            this._incomesQueryHandler = incomesQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await this._incomesQueryHandler.Execute(new GetUserIncomesByFilterQuery(this._auth.User().Id));

            return View(new IncomesResponse(result));
        }
    }
}
