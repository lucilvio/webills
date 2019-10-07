using Microsoft.AspNetCore.Mvc;
using Lucilvio.Solo.Webills.UseCases.AddNewIncome;
using Lucilvio.Solo.Webills.UseCases.AddNewExpense;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        private readonly IAddNewIncome _addNewIncome;
        private readonly IAddNewExpense _addNewExpense;
        private readonly ISearchForUserTransactionsInformation _searchForUserTransactionsInformation;

        public HomeController(IAddNewIncome addNewIncome, IAddNewExpense addNewExpense, 
            ISearchForUserTransactionsInformation searchForUserTransactionsInformation)
        {
            this._addNewIncome = addNewIncome;
            this._addNewExpense = addNewExpense;
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
    }
}