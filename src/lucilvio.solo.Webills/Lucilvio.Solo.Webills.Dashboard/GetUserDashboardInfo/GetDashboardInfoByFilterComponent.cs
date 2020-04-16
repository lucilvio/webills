using System.Threading.Tasks;

using Dapper;

using Lucilvio.Solo.Webills.Dashboard.Infraestructure.DataAccess;

namespace Lucilvio.Solo.Webills.Dashboard.GetUserDashboardInfo
{
    internal class GetDashboardInfoByFilterComponent
    {
        private readonly DashBoardContext _context;

        public GetDashboardInfoByFilterComponent(DashBoardContext context)
        {
            this._context = context;
        }

        public async Task<GetDashboardInfoByFilterOutput> Execute(GetDashboardInfoByFilterInput input)
        {
            using (var con = this._context.Connection)
            {
                var query = @"
                    select	TotalSpent.qtd as TotalSpent,
		            TotalEarns.qtd as TotalEarns,
		            (TotalEarns.qtd - TotalSpent.qtd) as Balance
                    from
                    (select case when sum(value) is NULL then 0 else sum(value) end qtd from dashboard.Transactions where IsExpense = 1 and userId = @userId) TotalSpent,
		            (select case when sum(value) is NULL then 0 else sum(value) end qtd from dashboard.Transactions where IsIncome = 1 and userId = @userId) TotalEarns";

                return await con.QueryFirstOrDefaultAsync<GetDashboardInfoByFilterOutput>(query, new { userId = input.UserId });
            }
        }
    }
}
