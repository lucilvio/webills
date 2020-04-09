using System;
using System.Threading.Tasks;
using Lucilvio.Solo.Webills.Clients.Web.Expenses.AddNew;
using Lucilvio.Solo.Webills.Clients.Web.Expenses.Edit;
using Lucilvio.Solo.Webills.Clients.Web.Expenses.Index;
using Lucilvio.Solo.Webills.Clients.Web.Expenses.Remove;
using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewExpense;
using Lucilvio.Solo.Webills.Transactions.EditExpense;
using Lucilvio.Solo.Webills.Transactions.GetExpense;
using Lucilvio.Solo.Webills.Transactions.GetExpensesByFilter;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Expenses
{
    [Authorize]
    [Route("Expenses")]
    public class ExpensesController : Controller
    {
        private readonly IAuthentication _auth;
        private readonly TransactionsModule _transactionsModule;

        public ExpensesController(IAuthentication auth, TransactionsModule transactionsModule)
        {
            this._auth = auth;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var foundExpenses = await this._transactionsModule.GetExpensesByFilter(new GetExpensesByFilterInput(this._auth.User().Id));

            return this.View(new ExpensesResponse(foundExpenses));
        }

        [HttpGet("{id:Guid}")]
        public async Task<JsonResult> Get(Guid id)
        {
            var foundExpense = await this._transactionsModule.GetExpenseById(new GetExpenseByIdInput(this._auth.User().Id, id));

            return new JsonResult(new { expense = new GetExpenseResponse(foundExpense) });
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromForm]AddNewExpenseRequest request)
        {
            await this._transactionsModule.AddNewExpense(new AddNewExpenseInput(this._auth.User().Id, 
                request.Name, request.Category, request.Date.StringToDate(), request.Value.MoneyToDecimal()), null);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromForm]EditExpenseRequest request)
        {
            await this._transactionsModule.EditExpense(new EditExpenseInput(this._auth.User().Id, request.Id, request.Name,
                request.Category, request.Date.StringToDate(), request.Value.MoneyToDecimal()));

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<JsonResult> Remove(RemoveExpenseRequest request)
        {
            await this._transactionsModule.RemoveExpense(new RemoveExpenseInput(this._auth.User().Id, request.Id));

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}