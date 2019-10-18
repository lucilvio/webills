using System.Linq;
using System.Collections.Generic;
using Lucilvio.Solo.Webills.Web.Home.Index;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserTransactionsInformationViewModel
    {
        public UserTransactionsInformationViewModel(SearchForUserTransactionsInformationResult searchResult)
        {
            if (searchResult == null)
                return;

            this.Balance = searchResult.Balance.DecimalToMoney();
            this.Incomes = searchResult.Incomes.Select(i => new UserIncomeViewModel(i));
            this.Expenses = searchResult.Expenses.Select(e => new UserExpenseViewModel(e));
        }

        public string Balance { get; }
        public IEnumerable<UserIncomeViewModel> Incomes { get; }
        public IEnumerable<UserExpenseViewModel> Expenses { get; }
    }
}