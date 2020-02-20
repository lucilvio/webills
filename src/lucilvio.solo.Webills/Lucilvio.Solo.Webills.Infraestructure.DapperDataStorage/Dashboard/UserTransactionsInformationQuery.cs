using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace Lucilvio.Solo.Webills.Infraestructure.DapperDataStorage
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
            var query = @$"select   u.Id as UserId,
		                            TotalExpenses.Value as TotalSpent,
		                            TotalIncomes.Value as TotalIncomes,
		                            TotalIncomes.Value - TotalExpenses.Value as Balance
                           from	    Transactions.Users u
		                            left join (select i.UserId userId, Sum(i.Value) as Value from Incomes i where i.UserId = @userId group by i.UserId) 
                                    as TotalIncomes on TotalIncomes.userId = u.id
		                            left join (select e.UserId userId, Sum(e.Value) as Value from Expenses e where e.UserId = @userId group by e.UserId) 
                                    as TotalExpenses on TotalExpenses.userId = u.Id
                           where	u.Id = @userId;
                           select	e.Id,
		                            e.Name,
		                            e.Value,
		                            e.Category
                           from	    Expenses e
                           where	e.UserId = @userId
		                            and (e.Date >= DATEADD(DAY, 0, DATEDIFF(DAY, 0, CURRENT_TIMESTAMP))
		                            and e.Date < DATEADD(DAY, 1, DATEDIFF(DAY, 0, CURRENT_TIMESTAMP)))";

            using(var con = this._context.GetConnection())
            {
                var result =  await con.QueryMultipleAsync(query, new { userId = parameters.UserId }).ConfigureAwait(false);

                var values = result.Read<ValuesData>().FirstOrDefault();
                var todayExpenses = result.Read<TodayExpensesData>().ToList();

                return new UserDashboardQueryResult(values, todayExpenses);
            }
        }
    }
}