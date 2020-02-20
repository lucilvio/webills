using System.Collections.Generic;
using System.Linq;
using Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage;
using Lucilvio.Solo.Webills.Web.Home.Index;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserTransactionsInformationResponse
    {
        public UserTransactionsInformationResponse(UserDashboardQueryResult searchResult)
        {
            if (searchResult == null)
                return;

            this.Balance = searchResult.Values.Balance.DecimalToMoney();
            this.TotalSpent = searchResult.Values.TotalSpent.DecimalToMoney();
            this.TotalEarns = searchResult.Values.TotalIncomes.DecimalToMoney();

            this.TodayExpenses = searchResult.TodayExpenses.Select(e => new TodayExpensesResponse(e));
        }

        public string Balance { get; }
        public string TotalEarns { get; }
        public string TotalSpent { get; }
        public IEnumerable<TodayExpensesResponse> TodayExpenses { get; }
    }
}