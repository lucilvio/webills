using Microsoft.AspNetCore.Mvc;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class HomeController : Controller
    {
        private readonly IAddNewIncome _addNewIncome;
        private readonly ISearchForUserIncomes _searchForUserIncomes;

        public HomeController(IAddNewIncome addNewIncome, ISearchForUserIncomes searchForUserIncomes)
        {
            this._addNewIncome = addNewIncome;
            this._sarchForUserIncomes = searchForUserIncomes;
        }

        public IActionResult Index()
        {
            var userIncomes = this._searchForUserIncomes.Execute();

            return View();
        }

        public IActionResult AddNewIncome([FromForm]NewIncomeViewModel viewModel)
        {
            this._addNewIncome.Execute(new NewIncomeCommandAdapter(viewModel));

            return RedirectToAction(nameof(Index));
        }
    }
}