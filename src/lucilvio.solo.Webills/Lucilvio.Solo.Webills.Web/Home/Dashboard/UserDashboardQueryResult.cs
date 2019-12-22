using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Web.Home.Sample
{
    public class UserDashboardQueryResult
    {
        private UserDashboardQueryResult()
        {
            this.Values = new ValuesData();
            this.TodayExpenses = new List<TodayExpensesData>();
        }

        public UserDashboardQueryResult(ValuesData values, IEnumerable<TodayExpensesData> todayExpenses) : this()
        {
            this.Values = values;
            this.TodayExpenses = todayExpenses;
        }

        public ValuesData Values { get; }
        public IEnumerable<TodayExpensesData> TodayExpenses { get; }
    }
}