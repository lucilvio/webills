using System.Collections.Generic;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
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
            if(values != null)
                this.Values = values;

            if(todayExpenses != null)
                this.TodayExpenses = todayExpenses;
        }

        public ValuesData Values { get; }
        public IEnumerable<TodayExpensesData> TodayExpenses { get; }
    }
}