using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;
using Lucilvio.Solo.Webills.Web.Logon;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthentication _authentication;
        private readonly TransactionsModule _transactionsModule;

        public HomeController(IAuthentication authentication, TransactionsModule transactionsModule)
        {
            this._authentication = authentication;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard([FromServices]IUserDashboardQueryHandler userDashBoardQueryHandler)
        {
            var loggedUser = this._authentication.User();
            var result = await userDashBoardQueryHandler.Execute(new UserDashboardQuery(loggedUser.Id));

            return View(new UserTransactionsInformationResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome([FromForm]AddNewIncomeRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new AddNewIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense([FromForm]AddNewExpenseRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new AddNewExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome([FromQuery]GetIncomeRequest request)
        {
            var foundIncome = await this._transactionsModule.ExecuteQuery<GetIncomeByIdQueryResult>(new GetIncomeByIdQuery(request.Id));

            if (foundIncome == null)
                return new JsonResult(new { error = "Income not found" });

            return new JsonResult(new { income = foundIncome });
        }

        [HttpGet]
        public async Task<JsonResult> EditExpense([FromQuery]GetExpenseRequest request)
        {
            var foundExpense = await this._transactionsModule.ExecuteQuery<GetExpenseByIdQueryResult>(new GetExpenseByIdQuery(request.Id));

            if (foundExpense == null)
                return new JsonResult(new { error = "Expense not found" });

            return new JsonResult(new { expense = foundExpense });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome([FromForm]EditIncomeRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new EditIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense([FromForm]EditExpenseRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new EditExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<JsonResult> RemoveIncome(RemoveIncomeRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new RemoveIncomeCommandAdapter(request));

            return new JsonResult(new { message = "Income removed" });
        }

        [HttpPost]
        public async Task<JsonResult> RemoveExpense(RemoveExpenseRequest request)
        {
            await this._transactionsModule.ExecuteCommand(new RemoveExpenseCommandAdapter(request));

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}