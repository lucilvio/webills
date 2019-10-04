using Microsoft.AspNetCore.Mvc; 

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

        public IActionResult Index()
        {
            var result = this._searchForUserTransactionsInformation.Execute();

            return View(new UserTransactionsInformationViewModel(result));
        }

        public IActionResult AddNewIncome([FromForm]AddNewIncomeViewModel viewModel)
        {
            this._addNewIncome.Execute(new AddNewIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddNewExpense([FromForm]AddNewExpenseViewModel viewModel)
        {
            this._addNewExpense.Execute(new AddNewExpenseCommandAdapter(viewModel));

            return RedirectToAction(nameof(Index));
        }
    }
}