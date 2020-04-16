using System;
using System.Threading.Tasks;

using Lucilvio.Solo.Webills.Clients.Web.Login;
using Lucilvio.Solo.Webills.Dashboard;
using Lucilvio.Solo.Webills.Dashboard.AddExpense;
using Lucilvio.Solo.Webills.Dashboard.EditTransaction;
using Lucilvio.Solo.Webills.Dashboard.RemoveTransaction;
using Lucilvio.Solo.Webills.Transactions;
using Lucilvio.Solo.Webills.Transactions.AddNewIncome;
using Lucilvio.Solo.Webills.Transactions.EditIncome;
using Lucilvio.Solo.Webills.Transactions.GetIncomeById;
using Lucilvio.Solo.Webills.Transactions.GetIncomesByFilter;
using Lucilvio.Solo.Webills.Transactions.RemoveExpense;
using Lucilvio.Solo.Webills.Transactions.RemoveIncome;
using Lucilvio.Solo.Webills.Web;
using Lucilvio.Solo.Webills.Web.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Lucilvio.Solo.Webills.Clients.Web.Incomes
{
    [Authorize]
    [Route("Incomes")]
    public class IncomesController : Controller
    {
        private readonly IAuthentication _auth;
        private readonly ILogger<IncomesController> _logger;

        private readonly DashboardModule _dashboardModule;
        private readonly TransactionsModule _transactionsModule;

        public IncomesController(ILoggerFactory logFactory, IAuthentication auth, TransactionsModule transactionsModule,
            DashboardModule dashboardModule)
        {
            this._auth = auth;
            this._dashboardModule = dashboardModule;
            this._transactionsModule = transactionsModule;
            this._logger = logFactory.CreateLogger<IncomesController>();
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
            var input = new AddNewIncomeInput(this._auth.User().Id, request.Name, request.Date.StringToDate(), 
                request.Value.MoneyToDecimal());

            await this._transactionsModule.AddNewIncome(input, this.OnIncomeCreated);

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
                request.Date.StringToDate(), request.Value.MoneyToDecimal()), OnIncomeEdited);

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("Remove")]
        public async Task<JsonResult> Remove(RemoveIncomeRequest request)
        {
            await this._transactionsModule.RemoveIncome(new RemoveIncomeInput(this._auth.User().Id, request.Id),
                this.OnRemoveIncome);

            return new JsonResult(new { message = "Expense removed" });
        }

        private async Task OnIncomeCreated(CreatedIncome income)
        {
            try
            {
                var newTransaction = AddTransactionInput.Income(income.UserId, income.Id, income.Name, income.Date, income.Value);
                await this._dashboardModule.AddTransaction(newTransaction);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
        }

        private async Task OnIncomeEdited(EditedIncome income)
        {
            try
            {
                var editedTransaction = EditTransactionInput.Income(income.UserId, income.Id, income.Name, income.Date, income.Value);
                await this._dashboardModule.EditTransaction(editedTransaction);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
        }

        private async Task OnRemoveIncome(RemovedIncome income)
        {
            try
            {
                await this._dashboardModule.RemoveTransaction(new RemoveTransactionInput(income.UserId, income.Id));
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
            }
        }
    }
}