using Microsoft.AspNetCore.Mvc; 

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        private readonly IAddNewIncome _addNewIncome;
        private readonly IAddNewExpense _addNewExpense;
        private readonly ISearchForUserIncomes _searchForUserIncomes;

        public HomeController(IAddNewIncome addNewIncome, IAddNewExpense addNewExpense, ISearchForUserIncomes searchForUserIncomes)
        {
            this._addNewIncome = addNewIncome;
            this._addNewExpense = addNewExpense;
            this._searchForUserIncomes = searchForUserIncomes;
        }

        public IActionResult Index()
        {
            var userIncomes = this._searchForUserIncomes.Execute();

            return View(new UserIncomesViewModel(userIncomes));
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