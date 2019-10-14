using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.Web.Home.EditIncome;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewExpense;
using Lucilvio.Solo.Webills.UseCases.Contracts.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.Contracts.EditIncome;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        private readonly IEditIncome _editIncome;
        private readonly IAddNewIncome _addNewIncome;
        private readonly IAddNewExpense _addNewExpense;
        private readonly ISearchForUserIncomeByNumber _searchForUserIncomeByNumber;
        private readonly ISearchForUserTransactionsInformation _searchForUserTransactionsInformation;

        public HomeController(IAddNewIncome addNewIncome, IAddNewExpense addNewExpense, IEditIncome editIncome,
            ISearchForUserTransactionsInformation searchForUserTransactionsInformation, ISearchForUserIncomeByNumber searchForUserIncome)
        {
            this._editIncome = editIncome;
            this._addNewIncome = addNewIncome;
            this._addNewExpense = addNewExpense;
            this._searchForUserIncomeByNumber = searchForUserIncome;
            this._searchForUserTransactionsInformation = searchForUserTransactionsInformation;
        }

        public IActionResult Dashboard()
        {
            var result = this._searchForUserTransactionsInformation.Execute();

            return View(new UserTransactionsInformationViewModel(result));
        }

        public async Task<IActionResult> AddNewIncome([FromForm]AddNewIncomeViewModel viewModel)
        {
            await this._addNewIncome.Execute(new AddNewIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> AddNewExpense([FromForm]AddNewExpenseViewModel viewModel)
        {
            await this._addNewExpense.Execute(new AddNewExpenseCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpGet]
        public async Task<JsonResult> EditIncome(string incomeNumber)
        {
            var foundIncome = this._searchForUserIncomeByNumber.Execute(new SearchForUserIncomeByNumberQuery(Guid.Parse(incomeNumber)));

            if (foundIncome == null)
                return new JsonResult(new { error = "Income not found" });

            return new JsonResult(new { income = new EditIncomeViewModel(foundIncome) });
        }

        [HttpPost]
        public async Task<ActionResult> EditIncome(EditIncomeViewModel viewModel)
        {
            await this._editIncome.Execute(new EditIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Dashboard));
        }
    }
}