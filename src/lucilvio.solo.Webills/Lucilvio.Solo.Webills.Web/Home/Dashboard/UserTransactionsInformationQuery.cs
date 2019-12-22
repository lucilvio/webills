using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Web.Home.Sample
{
    public class UserTransactionsInformationQuery : IUserDashboardQueryHandler       
    {
        private readonly WebillsReadContext _context;

        public UserTransactionsInformationQuery(WebillsReadContext context)
        {
            this._context = context;
        }

        public async Task<UserDashboardQueryResult> Execute(UserDashboardQuery parameters)
        {
            var query = @$"select	u.Id as UserId,
		                            TotalExpenses.Value as TotalSpent,
		                            TotalIncomes.Value as TotalIncomes,
		                            TotalIncomes.Value - TotalExpenses.Value as Balance
                           from	    Users u,
		                            (select i.UserId, Sum(i.Value) as Value from Incomes i group by i.UserId) as TotalIncomes,
		                            (select e.UserId, Sum(e.Value) as Value from Expenses e group by e.UserId) as TotalExpenses
                           where	u.Id = TotalIncomes.UserId
		                            and u.Id = TotalExpenses.UserId
		                            and u.Id = @userId;
                            select	e.Id,
		                            e.Name,
		                            e.Value,
                                    e.Number,
		                            e.Category
                            from	Expenses e
                            where	e.UserId = @userId
		                            and (e.Date >= DATEADD(DAY, 0, DATEDIFF(DAY, 0, CURRENT_TIMESTAMP))
		                            and e.Date < DATEADD(DAY, 1, DATEDIFF(DAY, 0, CURRENT_TIMESTAMP)))";

            using(var con = this._context.GetConnection())
            {
                var result =  await con.QueryMultipleAsync(query, new { userId = parameters.UserId }).ConfigureAwait(false);

                var values = result.Read<ValuesData>().First();
                var todayExpenses = result.Read<TodayExpensesData>().ToList();

                return new UserDashboardQueryResult(values, todayExpenses);
            }
        }
    }
}