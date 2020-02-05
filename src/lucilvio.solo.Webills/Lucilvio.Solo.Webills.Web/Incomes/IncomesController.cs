using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Lucilvio.Solo.Webills.Web.Logon;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;

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
