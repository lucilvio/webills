using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Web.Home.EditExpense;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.Web.Logon;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthentication _authentication;

        public HomeController(IAuthentication authentication)
        {
            this._authentication = authentication;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard([FromServices]IUserDashboardQueryHandler userDashBoardQueryHandler)
        {
            var loggedUser = this._authentication.User();
            var result = await userDashBoardQueryHandler.Execute(new UserDashboardQuery(loggedUser.Id));

            return View(new UserTransactionsInformationResponse(result));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome([FromServices]IAddNewIncomeUseCase useCase, [FromForm]AddNewIncomeRequest request)
        {
            await useCase.Execute(new AddNewIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense([FromServices]IAddNewExpenseUseCase useCase, [FromForm]AddNewExpenseRequest request)
        {
            await useCase.Execute(new AddNewExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome([FromQuery]GetIncomeRequest request)
        {
            //var foundIncome = await this._getUserIncomesQueryHandler.Execute(new GetUserIncomesQueryByNumber(1, request.Number));

            //if (foundIncome == null)
            //    return new JsonResult(new { error = "Income not found" });

            //return new JsonResult(new { income = new EditIncomeResponse(null) });
            return new JsonResult(new { });
        }

        [HttpGet]
        public async Task<JsonResult> EditExpense([FromQuery]GetExpenseRequest request)
        {
            //var foundExpense = await this._getUserExpensesQueryHandler.Execute(new GetUserExpensesByNumberQuery(1, request.Id));

            //if (foundExpense == null)
            //    return new JsonResult(new { error = "Expense not found" });

            //return new JsonResult(new { expense = new EditExpenseResponse(null) });
            return new JsonResult(new { });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome([FromServices]IEditIncomeUseCase useCase, [FromForm]EditIncomeRequest request)
        {
            await useCase.Execute(new EditIncomeCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense([FromServices]IEditExpenseUseCase useCase, [FromForm]EditExpenseRequest request)
        {
            await useCase.Execute(new EditExpenseCommandAdapter(request));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<JsonResult> RemoveIncome([FromServices]IRemoveIncomeUseCase useCase, RemoveIncomeRequest request)
        {
            await useCase.Execute(new RemoveIncomeCommandAdapter(request));

            return new JsonResult(new { message = "Income removed" });
        }

        [HttpPost]
        public async Task<JsonResult> RemoveExpense([FromServices]IRemoveExpenseUseCase useCase, RemoveExpenseRequest request)
        {
            await useCase.Execute(new RemoveExpenseCommandAdapter(request));

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}