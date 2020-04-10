using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;
using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    [Authorize]
    [Route("Incomes")]
    public class IncomesController : Controller
    {
        private readonly IAuthentication _auth;
        private readonly TransactionsModule _transactionsModule;

        public IncomesController(IAuthentication auth, TransactionsModule transactionsModule)
        {
            this._auth = auth;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var result = await this._transactionsModule.GetIncomesByFilter(new GetIncomesByFilterInput(this._auth.User().Id));

            return this.View(new IncomesResponse(result));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm]AddNewIncomeRequest request)
        {
            await this._transactionsModule.AddNewIncome(new AddNewIncomeInput(this._auth.User().Id,
                request.Name, request.Date.StringToDate(), request.Value.MoneyToDecimal()));

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:Guid}")]
        public async Task<JsonResult> Get(Guid id)
        {
            var foundIncome = await this._transactionsModule.GetIncomeById(new GetIncomeByIdInput(this._auth.User().Id, id));

            return new JsonResult(new { income = new GetIncomeResponse(foundIncome) });
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromForm]EditIncomeRequest request)
        {
            await this._transactionsModule.EditIncome(new EditIncomeInput(this._auth.User().Id, request.Id, request.Name,
                request.Date.StringToDate(), request.Value.MoneyToDecimal()));

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<JsonResult> Remove(RemoveIncomeRequest request)
        {
            await this._transactionsModule.RemoveIncome(new RemoveIncomeInput(this._auth.User().Id, request.Id));

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}