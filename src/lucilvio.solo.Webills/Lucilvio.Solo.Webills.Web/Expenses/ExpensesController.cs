using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Expenses.Index;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IAuthentication _auth;

        public ExpensesController(IAuthentication auth)
        {
            this._auth = auth;
        }

        public async Task<IActionResult> Index([FromServices]TransactionsModule transactionsModule)
        {
            var foundExpenses = await transactionsModule.GetExpensesByFilter(new GetExpensesByFilterInput(this._auth.User().Id));

            return this.View(new ExpensesResponse(foundExpenses));
        }
    }
}