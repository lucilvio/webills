using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Shared.Authentication;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Clients.Web.Shared.Messages;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;
using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    [Authorize]
    [Route("Incomes")]
    public class IncomesController : Controller
    {
        private readonly IAuthenticationService _auth;
        private readonly TransactionsModule _transactionsModule;

        public IncomesController(ILoggerFactory logFactory, IAuthenticationService auth, TransactionsModule transactionsModule)
        {
            this._auth = auth;
            this._transactionsModule = transactionsModule;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            var message = new GetIncomesByFilterInput(this._auth.User().Id);
            var result = await this._transactionsModule.SendMessage<GetIncomesByFilterInput, GetIncomesByFilterOutput>(message);

            return this.View(new IncomesResponse(result));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm]AddNewIncomeRequest request)
        {
            var message = new AddNewIncomeInput(this._auth.User().Id, request.Name, request.Date.StringToDate(),
                request.Value.MoneyToDecimal());

            await this._transactionsModule.SendMessage(message);

            this.SendSuccessMessage($"Income '{request.Name}' successfully added");

            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet("{id:Guid}")]
        public async Task<JsonResult> Get(Guid id)
        {
            var message = new GetIncomeByIdInput(this._auth.User().Id, id);
            var output = await this._transactionsModule.SendMessage<GetIncomeByIdInput, GetIncomeByIdOutput>(message);

            return new JsonResult(new { income = new GetIncomeResponse(output) });
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromForm]EditIncomeRequest request)
        {
            var message = new EditIncomeInput(this._auth.User().Id, request.Id, request.Name, request.Date.StringToDate(), 
                request.Value.MoneyToDecimal());

            await this._transactionsModule.SendMessage(message);

            this.SendSuccessMessage("Income successfully edited");

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<JsonResult> Remove(RemoveIncomeRequest request)
        {
            var message = new RemoveIncomeInput(this._auth.User().Id, request.Id);
            await this._transactionsModule.SendMessage(message);

            this.SendSuccessMessage($"Income successfully deleted");

            return new JsonResult(new { message = "Expense removed" });
        }
    }
}