using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Home.EditExpense;
using Lucilvio.Solo.Webills.Clients.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Dashboard;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly DashboardModule _dashboardModule;
        private readonly TransactionsModule _transactionsModule;

        public HomeController(IAuthentication authentication, DashboardModule dashboardModule, TransactionsModule transactionsModule)
        {
            this._authentication = authentication;
            this._dashboardModule = dashboardModule;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var loggedUser = this._authentication.User();
            //var result = await this._dashboardModule.ExecuteQuery<GetUserDashboardQueryResult>(new GetUserDashboardQuery(loggedUser.Id));

            return View(new UserTransactionsInformationResponse(null));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome([FromForm]AddNewIncomeRequest request)
        {
            await this._transactionsModule.AddNewIncome(new AddNewIncomeInputAdapter(this._authentication.User(), request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense([FromForm]AddNewExpenseRequest request)
        {
            await this._transactionsModule.AddNewExpense(
                new AddNewExpenseInputAdapter(this._authentication.User(), request), this.OnNewExpenseAdded);

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome([FromQuery]GetIncomeRequest request)
        {
            var foundIncome = await this._transactionsModule.GetIncome(new GetIncomeInputAdapter(this._authentication.User(), request));

            if (foundIncome == null)
                return new JsonResult(new { error = "Income not found" });

            return new JsonResult(new { income = foundIncome });
        }

        [HttpGet]
        public async Task<JsonResult> EditExpense([FromQuery]GetExpenseRequest request)
        {
            var foundExpense = await this._transactionsModule.GetExpense(new GetExpenseInputAdapter(this._authentication.User(), request));

            if (foundExpense == null)
                return new JsonResult(new { error = "Expense not found" });

            return new JsonResult(new { expense = foundExpense });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome([FromForm]EditIncomeRequest request)
        {
            await this._transactionsModule.EditIncome(new EditIncomeInputAdapter(this._authentication.User(), request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense([FromForm]EditExpenseRequest request)
        {
            await this._transactionsModule.EditExpense(new EditExpenseCommandAdapter(this._authentication.User(), request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<JsonResult> RemoveIncome(RemoveIncomeRequest request)
        {
            await this._transactionsModule.RemoveIncome(new RemoveIncomeInputAdapter(this._authentication.User(), request));

            return new JsonResult(new { message = "Income removed" });
        }

        [HttpPost]
        public async Task<JsonResult> RemoveExpense(RemoveExpenseRequest request)
        {
            await this._transactionsModule.RemoveExpense(new RemoveExpenseInputAdapter(this._authentication.User(), request));

            return new JsonResult(new { message = "Expense removed" });
        }

        private async Task OnNewExpenseAdded(NewAddedExpense addedExpense)
        {
            try
            {
                await this._dashboardModule.AddExpense(new AddedExpenseInputAdapter(addedExpense));

            }
            catch (System.Exception)
            {
                return;
            }
        }
    }
}