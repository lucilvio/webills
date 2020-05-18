using System.Collections.Generic;
using Lucilvio.Solo.Webills.Clients.Web.Shared.DataFormaters;
using Lucilvio.Solo.Webills.Dashboard.GetUserDashboardInfo;
using Lucilvio.Solo.Webills.Web.Home.Index;

namespace Lucilvio.Solo.Webills.Web.Home
{
    public class UserTransactionsInformationResponse
    {
        public UserTransactionsInformationResponse(GetDashboardInfoByFilterOutput dashboardInfo)
        {
            if (dashboardInfo == null)
                return;

            this.Balance = dashboardInfo.Balance.DecimalToMoney();
            this.TotalSpent = dashboardInfo.TotalSpent.DecimalToMoney();
            this.TotalEarns = dashboardInfo.TotalEarns.DecimalToMoney();

            //this.TodayExpenses = searchResult.TodayExpenses.Select(e => new TodayExpensesResponse(e));
            this.TodayExpenses = new List<TodayExpensesResponse>();
        }

        public string Balance { get; }
        public string TotalEarns { get; }
        public string TotalSpent { get; }
        public IEnumerable<TodayExpensesResponse> TodayExpenses { get; }
    }
}