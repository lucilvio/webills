using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Lucilvio.Solo.Webills.Web.Home.Sample;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.Web.Home.EditExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.RemoveExpense;
using Microsoft.AspNetCore.Authorization;
using Lucilvio.Solo.Webills.Web.Logon;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IEditIncome _editIncome;
        private readonly IEditExpense _editExpense;
        private readonly IAddNewIncome _addNewIncome;
        private readonly IRemoveIncome _removeIncome;
        private readonly IAddNewExpense _addNewExpense;
        private readonly IRemoveExpense _removeExpense;
        private readonly IAuthentication _authentication;
        private readonly IGetUserIncomesQueryHandler _getUserIncomesQueryHandler;
        private readonly IGetUserExpensesQueryHandler _getUserExpensesQueryHandler;
        private readonly IUserDashboardQueryHandler _userDashboardQueryHandler;

        public HomeController(IAddNewIncome addNewIncome, IAddNewExpense addNewExpense, IEditIncome editIncome, IEditExpense editExpense,
            IUserDashboardQueryHandler userDashboardQueryHandler, IGetUserIncomesQueryHandler searchForUserIncome,
            IGetUserExpensesQueryHandler searchForUserExpenseByNumber, IRemoveIncome removeIncome, IRemoveExpense removeExpense,
            IAuthentication authentication)
        {
            this._editIncome = editIncome;
            this._editExpense = editExpense;
            this._addNewIncome = addNewIncome;
            this._removeIncome = removeIncome;
            this._addNewExpense = addNewExpense;
            this._removeExpense = removeExpense;
            this._authentication = authentication;
            this._getUserIncomesQueryHandler = searchForUserIncome;
            this._getUserExpensesQueryHandler = searchForUserExpenseByNumber;
            this._userDashboardQueryHandler = userDashboardQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var loggedUser = this._authentication.User();
            var result = await this._userDashboardQueryHandler.Execute(new UserDashboardQuery(loggedUser.Id));

            return View(new UserTransactionsInformationResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome([FromForm]AddNewIncomeRequest request)
        {
            await this._addNewIncome.Execute(new AddNewIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense([FromForm]AddNewExpenseRequest request)
        {
            await this._addNewExpense.Execute(new AddNewExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome([FromQuery]GetIncomeRequest request)
        {
            var foundIncome = await this._getUserIncomesQueryHandler.Execute(new GetUserIncomesQueryByNumber(1, request.Number));

            if (foundIncome == null)
                return new JsonResult(new { error = "Income not found" });

            return new JsonResult(new { income = new EditIncomeResponse(foundIncome) });
        }

        [HttpGet]
        public async Task<JsonResult> EditExpense([FromQuery]GetExpenseRequest request)
        {
            var foundExpense = await this._getUserExpensesQueryHandler.Execute(new GetUserExpensesByNumberQuery(1, request.Id));

            if (foundExpense == null)
                return new JsonResult(new { error = "Expense not found" });
                
            return new JsonResult(new { expense = new EditExpenseResponse(foundExpense) });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome([FromForm]EditIncomeRequest request)
        {
            await this._editIncome.Execute(new EditIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense([FromForm]EditExpenseRequest request)
        {
            await this._editExpense.Execute(new EditExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<JsonResult> RemoveIncome(RemoveIncomeRequest request)
        {
            await this._removeIncome.Execute(new RemoveIncomeCommandAdapter(request));

            return new JsonResult(new { message = "Income removed" });
        }

        [HttpPost]
        public async Task<JsonResult> RemoveExpense(RemoveExpenseRequest request)
        {
            await this._removeExpense.Execute(new RemoveExpenseCommandAdapter(request));

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}