using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Expenses.Index;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Web.Logon;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IAuthentication _auth;
        private readonly IGetUserExpensesByFilterQueryHandler _getUserExpensesByFilterQueryHandler;

        public ExpensesController(IAuthentication auth, IGetUserExpensesByFilterQueryHandler getUserExpensesByFilterQueryHandler)
        {
            this._auth = auth;
            this._getUserExpensesByFilterQueryHandler = getUserExpensesByFilterQueryHandler;
        }

        public async Task<IActionResult> Index()
        {
            var result = await this._getUserExpensesByFilterQueryHandler.Execute(new GetUserExpensesByFilterQuery(this._auth.User().Id));

            return View(new ExpensesResponse(result));
        }
    }
}