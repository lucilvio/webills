using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditExpense;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        private readonly IEditIncome _editIncome;
        private readonly IEditExpense _editExpense;
        private readonly IAddNewIncome _addNewIncome;
        private readonly IAddNewExpense _addNewExpense;
        private readonly ISearchForUserIncomeByNumber _searchForUserIncomeByNumber;
        private readonly ISearchForUserExpenseByNumber _searchForUserExpenseByNumber;
        private readonly ISearchForUserTransactionsInformation _searchForUserTransactionsInformation;

        public HomeController(IAddNewIncome addNewIncome, IAddNewExpense addNewExpense, IEditIncome editIncome, IEditExpense editExpense,
            ISearchForUserTransactionsInformation searchForUserTransactionsInformation, ISearchForUserIncomeByNumber searchForUserIncome,
            ISearchForUserExpenseByNumber searchForUserExpenseByNumber)
        {
            this._editIncome = editIncome;
            this._editExpense = editExpense;
            this._addNewIncome = addNewIncome;
            this._addNewExpense = addNewExpense;
            this._searchForUserIncomeByNumber = searchForUserIncome;
            this._searchForUserExpenseByNumber = searchForUserExpenseByNumber;
            this._searchForUserTransactionsInformation = searchForUserTransactionsInformation;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var result = this._searchForUserTransactionsInformation.Execute();

            return View(new UserTransactionsInformationViewModel(result));
        
        }

        [HttpPost]
        public async Task<IActionResult> AddNewIncome([FromForm]AddNewIncomeViewModel viewModel)
        {
            await this._addNewIncome.Execute(new AddNewIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewExpense([FromForm]AddNewExpenseViewModel viewModel)
        {
            await this._addNewExpense.Execute(new AddNewExpenseCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome(string incomeNumber)
        {
            var foundIncome = await this._searchForUserIncomeByNumber.Execute(new SearchForUserIncomeByNumberQuery(Guid.Parse(incomeNumber)));

            if (foundIncome == null)
                return new JsonResult(new { error = "Income not found" });

            return new JsonResult(new { income = new EditIncomeViewModel(foundIncome) });
        }

        [HttpGet]
        public async Task<JsonResult> EditExpense(string expenseNumber)
        {
            var foundExpense = await this._searchForUserExpenseByNumber.Execute(new SearchForUserExpenseByNumberQuery(Guid.Parse(expenseNumber)));

            if (foundExpense == null)
                return new JsonResult(new { error = "Expense not found" });

            return new JsonResult(new { expense = new EditExpenseViewModel(foundExpense) });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome([FromForm]EditIncomeViewModel viewModel)
        {
            await this._editIncome.Execute(new EditIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<ActionResult> EditExpense([FromForm]EditExpenseViewModel viewModel)
        {
            await this._editExpense.Execute(new EditExpenseCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }
    }
}