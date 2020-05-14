using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
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
        private readonly IAuthenticationService _auth;
        private readonly TransactionsModule _transactionsModule;

        public ExpensesController(IAuthenticationService auth, TransactionsModule transactionsModule)
        {
            this._auth = auth;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var message = new GetExpensesByFilterInput(this._auth.User().Id);
            var foundExpenses = await this._transactionsModule.SendMessage<GetExpensesByFilterInput, GetExpensesByFilterOutput>(message);

            return this.View(new ExpensesResponse(foundExpenses));
        }

        [HttpGet("{id:Guid}")]
        public async Task<JsonResult> Get(Guid id)
        {
            var message = new GetExpenseByIdInput(this._auth.User().Id, id);
            var foundExpense = await this._transactionsModule.SendMessage<GetExpenseByIdInput, GetExpenseByIdOutput>(message);

            return new JsonResult(new { expense = new GetExpenseResponse(foundExpense) });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]AddNewExpenseRequest request)
        {
            var message = new AddNewExpenseInput(this._auth.User().Id, request.Name, request.Category, request.Date.StringToDate(), request.Value.MoneyToDecimal());
            await this._transactionsModule.SendMessage(message);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromForm]EditExpenseRequest request)
        {
            var message = new EditExpenseInput(this._auth.User().Id, request.Id, request.Name, request.Category, request.Date.StringToDate(), request.Value.MoneyToDecimal());
            await this._transactionsModule.SendMessage(message);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<JsonResult> Remove(RemoveExpenseRequest request)
        {
            var message = new RemoveExpenseInput(this._auth.User().Id, request.Id);
            await this._transactionsModule.SendMessage(message);

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}